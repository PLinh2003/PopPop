using PopPop.Models;

namespace PopPop.Services.Interfaces;

public interface IOrderService
{
    Task<int> PlaceOrderAsync(string userId, CheckoutViewModel checkout, CancellationToken cancellationToken = default);
    Task<OrderDetailsViewModel?> GetDetailsForUserAsync(int orderId, string userId, CancellationToken cancellationToken = default);
}
