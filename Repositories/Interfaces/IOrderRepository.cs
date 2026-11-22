using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;
using PopPop.Models;

namespace PopPop.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order, int>
{
    Task<Order?> GetDetailsForUserAsync(int orderId, string userId, CancellationToken cancellationToken = default);
    Task<Order?> GetByOrderCodeForPaymentAsync(string orderCode, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetExpiredVnPayOrdersAsync(DateTime utcNow, int batchSize, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetForUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetForManagementAsync(OrderStatus? status, string? search, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetRecentForManagementAsync(int count, CancellationToken cancellationToken = default);
    Task<Order?> GetDetailsForManagementAsync(int orderId, CancellationToken cancellationToken = default);
    Task<int> CountAllAsync(CancellationToken cancellationToken = default);
    Task<int> CountByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default);
    Task<decimal> GetCompletedRevenueAsync(DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OrderDailySummary>> GetDailySummariesAsync(DateTime fromUtc, DateTime toUtc, CancellationToken cancellationToken = default);
}
