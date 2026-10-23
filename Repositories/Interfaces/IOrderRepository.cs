using PopPop.Data.Entities;

namespace PopPop.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order, int>
{
    Task<Order?> GetDetailsForUserAsync(int orderId, string userId, CancellationToken cancellationToken = default);
}
