using PopPop.Models;
using PopPop.Data.Entities.Enums;

namespace PopPop.Services.Interfaces;

public interface IOrderService
{
    Task<int> PlaceOrderAsync(string userId, CheckoutViewModel checkout, CancellationToken cancellationToken = default);
    Task<OrderDetailsViewModel?> GetDetailsForUserAsync(int orderId, string userId, CancellationToken cancellationToken = default);
    Task<AdminDashboardViewModel> GetAdminDashboardAsync(CancellationToken cancellationToken = default);
    Task<OrderManagementListViewModel> GetManagementOrdersAsync(OrderStatus? status, string? search, CancellationToken cancellationToken = default);
    Task<OrderManagementDetailsViewModel?> GetManagementOrderDetailsAsync(int orderId, bool isAdmin, CancellationToken cancellationToken = default);
    Task UpdateOrderStatusAsync(int orderId, OrderStatus nextStatus, bool isAdmin, CancellationToken cancellationToken = default);
}
