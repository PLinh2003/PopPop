using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;
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

    public OrderService(
        ICartRepository cartRepository,
        IOrderRepository orderRepository,
        IRepository<InventoryTransaction, int> inventoryTransactionRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _inventoryTransactionRepository = inventoryTransactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> PlaceOrderAsync(
        string userId,
        CheckoutViewModel checkout,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException("Bạn cần đăng nhập để đặt hàng.");
        }

        var order = await _unitOfWork.ExecuteInTransactionAsync(async token =>
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
            var newOrder = new Order
            {
                UserId = userId,
                OrderCode = orderCode,
                Status = OrderStatus.Pending,
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
            cart.CartItems.Clear();
            cart.UpdatedAt = now;
            await _orderRepository.AddAsync(newOrder, token);
            return newOrder;
        }, cancellationToken);

        return order.Id;
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
            CreatedAt = order.CreatedAt,
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

    public async Task<AdminDashboardViewModel> GetAdminDashboardAsync(CancellationToken cancellationToken = default)
    {
        var recentOrders = await _orderRepository.GetRecentForManagementAsync(8, cancellationToken);
        var todayUtc = DateTime.UtcNow.Date;
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
                new DateTime(todayUtc.Year, todayUtc.Month, 1, 0, 0, 0, DateTimeKind.Utc), cancellationToken),
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
            AvailableStatuses = GetAvailableStatuses(order.Status, isAdmin)
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

            if (nextStatus == OrderStatus.Cancelled)
            {
                var now = DateTime.UtcNow;
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
                        Note = $"Hoàn kho do hủy đơn hàng {order.OrderCode}",
                        CreatedAt = now
                    }, token);
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
            OrderStatus.Completed => "Hoàn tất",
            OrderStatus.Cancelled => "Đã hủy",
            _ => "Không xác định"
        };
    }

    private static IReadOnlyList<OrderStatus> GetAvailableStatuses(OrderStatus currentStatus, bool isAdmin)
    {
        var statuses = currentStatus switch
        {
            OrderStatus.Pending => new List<OrderStatus> { OrderStatus.Confirmed },
            OrderStatus.Confirmed => new List<OrderStatus> { OrderStatus.Processing },
            OrderStatus.Processing => new List<OrderStatus> { OrderStatus.Shipping },
            OrderStatus.Shipping => new List<OrderStatus> { OrderStatus.Completed },
            _ => []
        };

        if (isAdmin && currentStatus is OrderStatus.Pending or OrderStatus.Confirmed or OrderStatus.Processing)
        {
            statuses.Add(OrderStatus.Cancelled);
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
}
