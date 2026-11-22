using PopPop.Models;
using PopPop.Data.Entities.Enums;

namespace PopPop.Services.Interfaces;

public interface IOrderService
{
    bool IsVnPayConfigured { get; }
    Task<CheckoutOrderResult> PlaceOrderAsync(string userId, CheckoutViewModel checkout, PaymentMethod paymentMethod, string clientIpAddress, string? callbackBaseUrl, CancellationToken cancellationToken = default);
    Task<VnPayIpnResponse> ProcessVnPayPaymentAsync(IReadOnlyDictionary<string, string> parameters, CancellationToken cancellationToken = default);
    Task ExpirePendingVnPayOrdersAsync(CancellationToken cancellationToken = default);
    Task<CustomerOrdersViewModel> GetOrdersForUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<OrderDetailsViewModel?> GetDetailsForUserAsync(int orderId, string userId, CancellationToken cancellationToken = default);
    Task<AdminDashboardViewModel> GetAdminDashboardAsync(CancellationToken cancellationToken = default);
    Task<OrderManagementListViewModel> GetManagementOrdersAsync(OrderStatus? status, string? search, CancellationToken cancellationToken = default);
    Task<OrderManagementDetailsViewModel?> GetManagementOrderDetailsAsync(int orderId, bool isAdmin, CancellationToken cancellationToken = default);
    Task UpdateOrderStatusAsync(int orderId, OrderStatus nextStatus, bool isAdmin, CancellationToken cancellationToken = default);
}
