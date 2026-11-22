using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities;
using PopPop.Data.Entities.Enums;
using PopPop.Models;
using PopPop.Repositories.Interfaces;

namespace PopPop.Repositories.Implementations;

public sealed class OrderRepository : Repository<Order, int>, IOrderRepository
{
    public OrderRepository(PopPopDbContext context)
        : base(context)
    {
    }

    public async Task<Order?> GetDetailsForUserAsync(
        int orderId,
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .Include(order => order.OrderItems)
            .FirstOrDefaultAsync(order => order.Id == orderId && order.UserId == userId, cancellationToken);
    }

    public async Task<Order?> GetByOrderCodeForPaymentAsync(
        string orderCode,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(order => order.OrderItems)
                .ThenInclude(item => item.ProductVariant)
            .FirstOrDefaultAsync(order => order.OrderCode == orderCode, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetExpiredVnPayOrdersAsync(
        DateTime utcNow,
        int batchSize,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(order => order.PaymentMethod == PaymentMethod.VnPay
                && order.PaymentStatus == PaymentStatus.Pending
                && order.Status == OrderStatus.Pending
                && order.PaymentExpiresAt <= utcNow
                && order.StockReleasedAt == null)
            .OrderBy(order => order.PaymentExpiresAt)
            .Take(batchSize)
            .Include(order => order.OrderItems)
                .ThenInclude(item => item.ProductVariant)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetForUserAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Where(order => order.UserId == userId)
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetForManagementAsync(
        OrderStatus? status,
        string? search,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet.AsNoTracking().Include(order => order.User).AsQueryable();
        if (status.HasValue)
        {
            query = query.Where(order => order.Status == status.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(order => order.OrderCode.Contains(term)
                || (order.RecipientName != null && order.RecipientName.Contains(term))
                || (order.RecipientPhone != null && order.RecipientPhone.Contains(term))
                || (order.User != null && order.User.FullName != null && order.User.FullName.Contains(term))
                || (order.User != null && order.User.Email != null && order.User.Email.Contains(term)));
        }

        return await query
            .OrderByDescending(order => order.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetRecentForManagementAsync(
        int count,
        CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking()
            .Include(order => order.User)
            .OrderByDescending(order => order.CreatedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetDetailsForManagementAsync(
        int orderId,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(order => order.User)
            .Include(order => order.OrderItems)
                .ThenInclude(item => item.ProductVariant)
            .FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }

    public Task<int> CountByStatusAsync(OrderStatus status, CancellationToken cancellationToken = default)
    {
        return DbSet.CountAsync(order => order.Status == status, cancellationToken);
    }

    public Task<int> CountAllAsync(CancellationToken cancellationToken = default)
    {
        return DbSet.CountAsync(cancellationToken);
    }

    public async Task<decimal> GetCompletedRevenueAsync(
        DateTime fromUtc,
        DateTime toUtc,
        CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Where(order => order.Status == OrderStatus.Completed
                && order.UpdatedAt >= fromUtc
                && order.UpdatedAt < toUtc)
            .SumAsync(order => (decimal?)order.TotalAmount, cancellationToken) ?? 0m;
    }

    public async Task<IReadOnlyList<OrderDailySummary>> GetDailySummariesAsync(
        DateTime fromUtc,
        DateTime toUtc,
        CancellationToken cancellationToken = default)
    {
        var orders = await DbSet.AsNoTracking()
            .Where(order => (order.CreatedAt >= fromUtc && order.CreatedAt < toUtc)
                || (order.Status == OrderStatus.Completed && order.UpdatedAt >= fromUtc && order.UpdatedAt < toUtc))
            .Select(order => new { order.CreatedAt, order.UpdatedAt, order.Status, order.TotalAmount })
            .ToListAsync(cancellationToken);

        return Enumerable.Range(0, (toUtc.Date - fromUtc.Date).Days)
            .Select(day => fromUtc.Date.AddDays(day))
            .Select(date => new OrderDailySummary
            {
                Date = date,
                OrderCount = orders.Count(order => order.CreatedAt.Date == date),
                CompletedRevenue = orders
                    .Where(order => order.Status == OrderStatus.Completed && order.UpdatedAt.Date == date)
                    .Sum(order => order.TotalAmount)
            })
            .ToList();
    }
}
