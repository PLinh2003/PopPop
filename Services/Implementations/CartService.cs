using PopPop.Data.Entities;
using PopPop.Models;
using PopPop.Repositories.Interfaces;
using PopPop.Services.Interfaces;

namespace PopPop.Services.Implementations;

public sealed class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CartService(
        ICartRepository cartRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CartViewModel> GetCartAsync(string userId, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
        return cart is null ? new CartViewModel() : MapCart(cart);
    }

    public async Task AddItemAsync(
        string userId,
        int productVariantId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Số lượng sản phẩm phải lớn hơn 0.");
        }

        var variant = await _productRepository.GetAvailableVariantAsync(productVariantId, cancellationToken)
            ?? throw new InvalidOperationException("Sản phẩm hiện không khả dụng.");

        if (quantity > variant.StockQuantity)
        {
            throw new InvalidOperationException("Số lượng yêu cầu vượt quá tồn kho.");
        }

        var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
        var now = DateTime.UtcNow;

        if (cart is null)
        {
            cart = new Cart
            {
                UserId = userId,
                CreatedAt = now,
                UpdatedAt = now
            };
            await _cartRepository.AddAsync(cart, cancellationToken);
        }

        var existingItem = cart.CartItems.FirstOrDefault(item => item.ProductVariantId == productVariantId);
        if (existingItem is not null)
        {
            if (existingItem.Quantity > variant.StockQuantity - quantity)
            {
                throw new InvalidOperationException("Số lượng trong giỏ vượt quá tồn kho hiện tại.");
            }

            existingItem.Quantity += quantity;
            existingItem.UnitPrice = variant.Price;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                ProductVariantId = productVariantId,
                Quantity = quantity,
                UnitPrice = variant.Price
            });
        }

        cart.UpdatedAt = now;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateQuantityAsync(
        string userId,
        int cartItemId,
        int quantity,
        CancellationToken cancellationToken = default)
    {
        if (quantity <= 0)
        {
            throw new InvalidOperationException("Số lượng sản phẩm phải lớn hơn 0.");
        }

        var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
        var item = cart?.CartItems.FirstOrDefault(cartItem => cartItem.Id == cartItemId);
        if (cart is null || item is null)
        {
            return false;
        }

        var variant = await _productRepository.GetAvailableVariantAsync(item.ProductVariantId, cancellationToken)
            ?? throw new InvalidOperationException("Sản phẩm hiện không khả dụng.");

        if (quantity > variant.StockQuantity)
        {
            throw new InvalidOperationException("Số lượng yêu cầu vượt quá tồn kho.");
        }

        item.Quantity = quantity;
        item.UnitPrice = variant.Price;
        cart.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> RemoveItemAsync(
        string userId,
        int cartItemId,
        CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetByUserIdAsync(userId, cancellationToken);
        var item = cart?.CartItems.FirstOrDefault(cartItem => cartItem.Id == cartItemId);
        if (cart is null || item is null)
        {
            return false;
        }

        cart.CartItems.Remove(item);
        cart.UpdatedAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static CartViewModel MapCart(Cart cart)
    {
        return new CartViewModel
        {
            Items = cart.CartItems.Select(item =>
            {
                var variant = item.ProductVariant;
                var product = variant?.Product;
                var isAvailable = variant is { IsActive: true, StockQuantity: > 0 } && product?.IsActive == true;

                return new CartLineViewModel
                {
                    Id = item.Id,
                    ProductVariantId = item.ProductVariantId,
                    ProductName = product?.Name ?? "Sản phẩm không khả dụng",
                    SKU = variant?.SKU ?? string.Empty,
                    ImageUrl = product?.ProductImages.FirstOrDefault()?.ImageUrl,
                    UnitPrice = isAvailable ? variant!.Price : item.UnitPrice,
                    Quantity = item.Quantity,
                    StockQuantity = isAvailable ? variant!.StockQuantity : 0
                };
            }).ToList()
        };
    }
}
