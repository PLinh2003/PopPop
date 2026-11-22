using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;
using PopPop.ExternalServices.Pay;
using PopPop.Models;
using PopPop.Repositories.Interfaces;
using PopPop.Services.Interfaces;

namespace PopPop.Services.Implementations;

public sealed class OrderService : IOrderService
{
    private const decimal ShippingFee = 0m;

    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IRepository<InventoryTransaction, int> _inventoryTransactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVnPayClient _vnPayClient;

    public OrderService(
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IRepository<InventoryTransaction, int> inventoryTransactionRepository,
        IUnitOfWork unitOfWork,
        IVnPayClient vnPayClient)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _inventoryTransactionRepository = inventoryTransactionRepository;
        _unitOfWork = unitOfWork;
        _vnPayClient = vnPayClient;
    }

    public bool IsVnPayConfigured => _vnPayClient.IsConfigured;

    public async Task<CheckoutOrderResult> PlaceOrderAsync(
        string userId,
        CheckoutViewModel checkout,
        PaymentMethod paymentMethod,
        string clientIpAddress,
        string? callbackBaseUrl,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException("Bạn cần đăng nhập để đặt hàng.");
        }

        if (paymentMethod is not (PaymentMethod.CashOnDelivery or PaymentMethod.VnPay))
        {
            throw new InvalidOperationException("Phương thức thanh toán không hợp lệ.");
        }

        if (paymentMethod == PaymentMethod.VnPay && !_vnPayClient.IsConfigured)
        {
            throw new InvalidOperationException("VNPay chưa được cấu hình. Vui lòng chọn COD hoặc cấu hình VNPay Sandbox.");
        }

        var (order, paymentUrl) = await _unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            var cart = await _cartRepository.GetByUserIdAsync(userId, token);
            if (cart is null || cart.CartItems.Count == 0)
            {
                throw new InvalidOperationException("Giỏ hàng của bạn đang trống.");
            }

            foreach (var cartItem in cart.CartItems)
            {
                var variant = cartItem.ProductVariant;
                if (variant is null || !variant.IsActive || variant.Product?.IsActive != true)
                {
                    throw new InvalidOperationException("Một sản phẩm trong giỏ hiện không còn khả dụng.");
                }

                if (cartItem.Quantity <= 0 || cartItem.Quantity > variant.StockQuantity)
                {
                    throw new InvalidOperationException($"Sản phẩm {variant.Product.Name} không đủ số lượng tồn kho.");
                }
            }

            var now = DateTime.UtcNow;
            var orderCode = $"PP-{now:yyyyMMddHHmmss}-{Guid.NewGuid():N}".ToUpperInvariant();
            var paymentExpiresAt = paymentMethod == PaymentMethod.VnPay
                ? now.AddMinutes(_vnPayClient.PaymentTimeoutMinutes)
                : (DateTime?)null;
            var newOrder = new Order
            {
                UserId = userId,
                OrderCode = orderCode,
                Status = OrderStatus.Pending,
                PaymentMethod = paymentMethod,
                PaymentStatus = paymentMethod == PaymentMethod.VnPay ? PaymentStatus.Pending : PaymentStatus.Unpaid,
                PaymentExpiresAt = paymentExpiresAt,
                RecipientName = checkout.RecipientName.Trim(),
                RecipientPhone = checkout.RecipientPhone.Trim(),
                ShippingAddress = checkout.ShippingAddress.Trim(),
                Note = string.IsNullOrWhiteSpace(checkout.Note) ? null : checkout.Note.Trim(),
                ShippingFee = ShippingFee,
                CreatedAt = now,
                UpdatedAt = now
            };

            foreach (var cartItem in cart.CartItems)
            {
                var variant = cartItem.ProductVariant!;
                var product = variant.Product!;
                var lineTotal = variant.Price * cartItem.Quantity;

                newOrder.OrderItems.Add(new OrderItem
                {
                    ProductVariantId = variant.Id,
                    ProductName = product.Name,
                    SKU = variant.SKU,
                    UnitPrice = variant.Price,
                    Quantity = cartItem.Quantity,
                    TotalPrice = lineTotal
                });

                variant.StockQuantity -= cartItem.Quantity;
                await _inventoryTransactionRepository.AddAsync(new InventoryTransaction
                {
                    ProductVariantId = variant.Id,
                    Type = InventoryTransactionType.Sale,
                    Quantity = -cartItem.Quantity,
                    Note = $"Xuất kho cho đơn hàng {orderCode}",
                    CreatedAt = now
                }, token);
            }

            newOrder.TotalAmount = newOrder.OrderItems.Sum(item => item.TotalPrice) + newOrder.ShippingFee;
            var paymentUrl = paymentMethod == PaymentMethod.VnPay
                ? _vnPayClient.CreatePaymentUrl(orderCode, newOrder.TotalAmount, clientIpAddress, callbackBaseUrl ?? string.Empty, now, paymentExpiresAt!.Value)
                : null;
            cart.CartItems.Clear();
            cart.UpdatedAt = now;
            await _orderRepository.AddAsync(newOrder, token);
            return (Order: newOrder, PaymentUrl: paymentUrl);
        }, cancellationToken);

        return new CheckoutOrderResult(order.Id, paymentUrl);
    }

    public async Task<VnPayIpnResponse> ProcessVnPayIpnAsync(
        IReadOnlyDictionary<string, string> parameters,
        CancellationToken cancellationToken = default)
    {
        if (!_vnPayClient.ValidateSignature(parameters))
        {
            return new VnPayIpnResponse("97", "Invalid Checksum");
        }

        if (!TryGetParameter(parameters, "vnp_TxnRef", out var transactionReference)
            || !TryGetParameter(parameters, "vnp_Amount", out var amountValue)
            || !long.TryParse(amountValue, out var amountInMinorUnits)
            || !TryGetParameter(parameters, "vnp_ResponseCode", out var responseCode)
            || !TryGetParameter(parameters, "vnp_TransactionStatus", out var transactionStatus))
        {
            return new VnPayIpnResponse("99", "Invalid request");
        }

        var transactionNo = GetParameter(parameters, "vnp_TransactionNo");
        var bankCode = GetParameter(parameters, "vnp_BankCode");
        var now = DateTime.UtcNow;

        return await _unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            var order = await _orderRepository.GetByOrderCodeForPaymentAsync(transactionReference, token);
            if (order is null || order.PaymentMethod != PaymentMethod.VnPay)
            {
                return new VnPayIpnResponse("01", "Order not found");
            }

            if (order.PaymentStatus == PaymentStatus.Paid)
            {
                return new VnPayIpnResponse("02", "Order already confirmed");
            }

            if (amountInMinorUnits != checked((long)(order.TotalAmount * 100m)))
            {
                return new VnPayIpnResponse("04", "Invalid amount");
            }

            if (order.Status == OrderStatus.Cancelled
                || order.StockReleasedAt.HasValue
                || order.PaymentStatus != PaymentStatus.Pending
                || order.PaymentExpiresAt <= now)
            {
                return new VnPayIpnResponse("02", "Order is no longer payable");
            }

            order.VnpayTransactionNo = transactionNo;
            order.VnpayBankCode = bankCode;
            order.VnpayResponseCode = responseCode;
            order.UpdatedAt = now;

            if (responseCode == "00" && transactionStatus == "00")
            {
                order.PaymentStatus = PaymentStatus.Paid;
                order.PaidAt = now;
                order.PaymentExpiresAt = null;
                return new VnPayIpnResponse("00", "Confirm Success");
            }

            order.PaymentStatus = PaymentStatus.Failed;
            order.Status = OrderStatus.Cancelled;
            await ReleaseReservedStockAsync(order, $"Hoàn kho do VNPay thất bại cho đơn hàng {order.OrderCode}", now, token);
            await RestoreOrderItemsToCartAsync(order, now, token);
            return new VnPayIpnResponse("00", "Confirm Success");
        }, cancellationToken);
    }

    public async Task ExpirePendingVnPayOrdersAsync(CancellationToken cancellationToken = default)
    {
        await _unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            var now = DateTime.UtcNow;
            var expiredOrders = await _orderRepository.GetExpiredVnPayOrdersAsync(now, 100, token);
            foreach (var order in expiredOrders)
            {
                order.PaymentStatus = PaymentStatus.Failed;
                order.VnpayResponseCode = "EXPIRED";
                order.Status = OrderStatus.Cancelled;
                order.UpdatedAt = now;
                await ReleaseReservedStockAsync(order, $"Hoàn kho do hết hạn thanh toán VNPay cho đơn hàng {order.OrderCode}", now, token);
                await RestoreOrderItemsToCartAsync(order, now, token);
            }

            return true;
        }, cancellationToken);
    }

    public async Task<OrderDetailsViewModel?> GetDetailsForUserAsync(
        int orderId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetDetailsForUserAsync(orderId, userId, cancellationToken);
        if (order is null)
        {
            return null;
        }

        return new OrderDetailsViewModel
        {
            OrderCode = order.OrderCode,
            Status = order.Status,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            PaidAt = order.PaidAt,
            CreatedAt = order.CreatedAt,
            UpdatedAt = order.UpdatedAt,
            RecipientName = order.RecipientName ?? string.Empty,
            RecipientPhone = order.RecipientPhone ?? string.Empty,
            ShippingAddress = order.ShippingAddress ?? string.Empty,
            Note = order.Note,
            Items = order.OrderItems.Select(item => new OrderLineViewModel
            {
                ProductName = item.ProductName,
                SKU = item.SKU,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                LineTotal = item.TotalPrice
            }).ToList(),
            ShippingFee = order.ShippingFee,
            TotalAmount = order.TotalAmount
        };
    }

    public async Task<CustomerOrdersViewModel> GetOrdersForUserAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetForUserAsync(userId, cancellationToken);
        return new CustomerOrdersViewModel
        {
            Orders = orders.Select(order => new CustomerOrderRowViewModel
            {
                Id = order.Id,
                OrderCode = order.OrderCode,
                Status = order.Status,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
                StatusText = GetStatusText(order.Status),
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                TotalAmount = order.TotalAmount
            }).ToList()
        };
    }

    public async Task<AdminDashboardViewModel> GetAdminDashboardAsync(CancellationToken cancellationToken = default)
    {
        var recentOrders = await _orderRepository.GetRecentForManagementAsync(8, cancellationToken);
        var todayUtc = DateTime.UtcNow.Date;
        var monthStartUtc = new DateTime(todayUtc.Year, todayUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var chartStartUtc = todayUtc.AddDays(-6);
        var dailySummaries = await _orderRepository.GetDailySummariesAsync(
            chartStartUtc,
            todayUtc.AddDays(1),
            cancellationToken);
        var summariesByDate = dailySummaries.ToDictionary(summary => summary.Date.Date);
        var chartDates = Enumerable.Range(0, 7).Select(day => chartStartUtc.AddDays(day)).ToList();
        var pendingOrders = await _orderRepository.CountByStatusAsync(OrderStatus.Pending, cancellationToken);
        var confirmedOrders = await _orderRepository.CountByStatusAsync(OrderStatus.Confirmed, cancellationToken);
        var processingOrders = await _orderRepository.CountByStatusAsync(OrderStatus.Processing, cancellationToken);
        var shippingOrders = await _orderRepository.CountByStatusAsync(OrderStatus.Shipping, cancellationToken);
        var completedOrders = await _orderRepository.CountByStatusAsync(OrderStatus.Completed, cancellationToken);
        var cancelledOrders = await _orderRepository.CountByStatusAsync(OrderStatus.Cancelled, cancellationToken);

        return new AdminDashboardViewModel
        {
            TotalOrders = await _orderRepository.CountAllAsync(cancellationToken),
            PendingOrders = pendingOrders,
            ConfirmedOrders = confirmedOrders,
            ProcessingOrders = processingOrders,
            ShippingOrders = shippingOrders,
            CompletedOrders = completedOrders,
            CancelledOrders = cancelledOrders,
            MonthlyRevenue = await _orderRepository.GetCompletedRevenueAsync(
                monthStartUtc,
                monthStartUtc.AddMonths(1),
                cancellationToken),
            DailyChartLabels = chartDates.Select(date => date.ToString("dd/MM")).ToList(),
            DailyOrderCounts = chartDates.Select(date => summariesByDate.GetValueOrDefault(date)?.OrderCount ?? 0).ToList(),
            DailyCompletedRevenue = chartDates.Select(date => summariesByDate.GetValueOrDefault(date)?.CompletedRevenue ?? 0m).ToList(),
            StatusChartValues = [pendingOrders, confirmedOrders, processingOrders, shippingOrders, completedOrders, cancelledOrders],
            RecentOrders = recentOrders.Select(ToManagementRow).ToList()
        };
    }

    public async Task<OrderManagementListViewModel> GetManagementOrdersAsync(
        OrderStatus? status,
        string? search,
        CancellationToken cancellationToken = default)
    {
        var orders = await _orderRepository.GetForManagementAsync(status, search, cancellationToken);
        return new OrderManagementListViewModel
        {
            Status = status,
            Search = search,
            Orders = orders.Select(ToManagementRow).ToList()
        };
    }

    public async Task<OrderManagementDetailsViewModel?> GetManagementOrderDetailsAsync(
        int orderId,
        bool isAdmin,
        CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetDetailsForManagementAsync(orderId, cancellationToken);
        if (order is null)
        {
            return null;
        }

        return new OrderManagementDetailsViewModel
        {
            Id = order.Id,
            OrderCode = order.OrderCode,
            Status = order.Status,
            StatusText = GetStatusText(order.Status),
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            PaidAt = order.PaidAt,
            CreatedAt = order.CreatedAt,
            CustomerName = order.User?.FullName ?? order.User?.Email ?? "Khách hàng",
            CustomerEmail = order.User?.Email ?? string.Empty,
            RecipientName = order.RecipientName ?? string.Empty,
            RecipientPhone = order.RecipientPhone ?? string.Empty,
            ShippingAddress = order.ShippingAddress ?? string.Empty,
            Note = order.Note,
            ShippingFee = order.ShippingFee,
            TotalAmount = order.TotalAmount,
            Items = order.OrderItems.Select(item => new OrderLineViewModel
            {
                ProductName = item.ProductName,
                SKU = item.SKU,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity,
                LineTotal = item.TotalPrice
            }).ToList(),
            AvailableStatuses = GetAvailableStatuses(order, isAdmin)
        };
    }

    public async Task UpdateOrderStatusAsync(
        int orderId,
        OrderStatus nextStatus,
        bool isAdmin,
        CancellationToken cancellationToken = default)
    {
        var updated = await _unitOfWork.ExecuteInTransactionAsync(async token =>
        {
            var order = await _orderRepository.GetDetailsForManagementAsync(orderId, token);
            if (order is null)
            {
                return false;
            }

            if (!IsTransitionAllowed(order.Status, nextStatus, isAdmin))
            {
                throw new InvalidOperationException("Không thể chuyển đơn hàng sang trạng thái đã chọn.");
            }

            if (nextStatus == OrderStatus.Confirmed
                && order.PaymentMethod == PaymentMethod.VnPay
                && order.PaymentStatus != PaymentStatus.Paid)
            {
                throw new InvalidOperationException("Chỉ được xác nhận đơn VNPay sau khi hệ thống xác nhận thanh toán thành công.");
            }

            if (nextStatus == OrderStatus.Cancelled
                && order.PaymentMethod == PaymentMethod.VnPay
                && order.PaymentStatus == PaymentStatus.Paid)
            {
                throw new InvalidOperationException("Đơn VNPay đã thanh toán cần được hoàn tiền trên cổng VNPay trước khi hủy.");
            }

            if (nextStatus == OrderStatus.Cancelled
                && order.PaymentMethod == PaymentMethod.VnPay
                && order.PaymentStatus == PaymentStatus.Pending)
            {
                throw new InvalidOperationException("Không thể hủy đơn khi VNPay đang xử lý thanh toán. Đơn sẽ tự hết hạn nếu giao dịch không hoàn tất.");
            }

            if (nextStatus == OrderStatus.Cancelled)
            {
                var now = DateTime.UtcNow;
                if (order.StockReleasedAt is null)
                {
                    await ReleaseReservedStockAsync(order, $"Hoàn kho do hủy đơn hàng {order.OrderCode}", now, token);
                }

                if (order.PaymentMethod == PaymentMethod.VnPay && order.PaymentStatus == PaymentStatus.Pending)
                {
                    order.PaymentStatus = PaymentStatus.Failed;
                    order.VnpayResponseCode = "CANCELLED";
                }
            }

            order.Status = nextStatus;
            order.UpdatedAt = DateTime.UtcNow;
            return true;
        }, cancellationToken);

        if (!updated)
        {
            throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
        }
    }

    private static OrderManagementRowViewModel ToManagementRow(Order order)
    {
        return new OrderManagementRowViewModel
        {
            Id = order.Id,
            OrderCode = order.OrderCode,
            CustomerName = order.RecipientName ?? order.User?.FullName ?? order.User?.Email ?? "Khách hàng",
            CustomerPhone = order.RecipientPhone ?? string.Empty,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            StatusText = GetStatusText(order.Status),
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
            TotalAmount = order.TotalAmount
        };
    }

    private static string GetStatusText(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Pending => "Chờ xác nhận",
            OrderStatus.Confirmed => "Đã xác nhận",
            OrderStatus.Processing => "Đang xử lý",
            OrderStatus.Shipping => "Đang giao",
            OrderStatus.Completed => "Đã giao thành công",
            OrderStatus.Cancelled => "Đã hủy",
            _ => "Không xác định"
        };
    }

    private static IReadOnlyList<OrderStatus> GetAvailableStatuses(Order order, bool isAdmin)
    {
        var statuses = order.Status switch
        {
            OrderStatus.Pending => new List<OrderStatus> { OrderStatus.Confirmed },
            OrderStatus.Confirmed => new List<OrderStatus> { OrderStatus.Processing },
            OrderStatus.Processing => new List<OrderStatus> { OrderStatus.Shipping },
            OrderStatus.Shipping => new List<OrderStatus> { OrderStatus.Completed },
            _ => []
        };

        if (order.PaymentMethod == PaymentMethod.VnPay && order.PaymentStatus != PaymentStatus.Paid)
        {
            statuses.Remove(OrderStatus.Confirmed);
        }

        if (order.PaymentMethod == PaymentMethod.VnPay && order.PaymentStatus == PaymentStatus.Paid)
        {
            statuses.Remove(OrderStatus.Cancelled);
        }

        if (isAdmin
            && order.Status is OrderStatus.Pending or OrderStatus.Confirmed or OrderStatus.Processing
            && !(order.PaymentMethod == PaymentMethod.VnPay
                && order.PaymentStatus is (PaymentStatus.Pending or PaymentStatus.Paid)))
        {
            if (!statuses.Contains(OrderStatus.Cancelled))
            {
                statuses.Add(OrderStatus.Cancelled);
            }
        }

        return statuses;
    }

    private static bool IsTransitionAllowed(OrderStatus currentStatus, OrderStatus nextStatus, bool isAdmin)
    {
        return (currentStatus, nextStatus) switch
        {
            (OrderStatus.Pending, OrderStatus.Confirmed) => true,
            (OrderStatus.Confirmed, OrderStatus.Processing) => true,
            (OrderStatus.Processing, OrderStatus.Shipping) => true,
            (OrderStatus.Shipping, OrderStatus.Completed) => true,
            (OrderStatus.Pending or OrderStatus.Confirmed or OrderStatus.Processing, OrderStatus.Cancelled) => isAdmin,
            _ => false
        };
    }

    private async Task ReleaseReservedStockAsync(
        Order order,
        string note,
        DateTime now,
        CancellationToken cancellationToken)
    {
        if (order.StockReleasedAt.HasValue)
        {
            return;
        }

        foreach (var item in order.OrderItems)
        {
            var variant = item.ProductVariant
                ?? throw new InvalidOperationException("Không tìm thấy phiên bản sản phẩm để hoàn tồn kho.");
            variant.StockQuantity += item.Quantity;
            await _inventoryTransactionRepository.AddAsync(new InventoryTransaction
            {
                ProductVariantId = variant.Id,
                Type = InventoryTransactionType.AdjustmentIncrease,
                Quantity = item.Quantity,
                Note = note,
                CreatedAt = now
            }, cancellationToken);
        }

        order.StockReleasedAt = now;
    }

    private async Task RestoreOrderItemsToCartAsync(
        Order order,
        DateTime now,
        CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByUserIdAsync(order.UserId, cancellationToken);
        if (cart is null)
        {
            return;
        }

        foreach (var item in order.OrderItems)
        {
            var cartItem = cart.CartItems.FirstOrDefault(existing => existing.ProductVariantId == item.ProductVariantId);
            if (cartItem is null)
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }
            else
            {
                cartItem.Quantity += item.Quantity;
            }
        }

        cart.UpdatedAt = now;
    }

    private static bool TryGetParameter(
        IReadOnlyDictionary<string, string> parameters,
        string key,
        out string value)
    {
        return parameters.TryGetValue(key, out value!) && !string.IsNullOrWhiteSpace(value);
    }

    private static string? GetParameter(IReadOnlyDictionary<string, string> parameters, string key)
    {
        return parameters.TryGetValue(key, out var value) ? value : null;
    }
}
