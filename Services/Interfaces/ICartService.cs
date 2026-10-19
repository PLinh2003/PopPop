using PopPop.Models;

namespace PopPop.Services.Interfaces;

public interface ICartService
{
    Task<CartViewModel> GetCartAsync(string userId, CancellationToken cancellationToken = default);
    Task AddItemAsync(string userId, int productVariantId, int quantity, CancellationToken cancellationToken = default);
    Task<bool> UpdateQuantityAsync(string userId, int cartItemId, int quantity, CancellationToken cancellationToken = default);
    Task<bool> RemoveItemAsync(string userId, int cartItemId, CancellationToken cancellationToken = default);
}
