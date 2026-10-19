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
}
