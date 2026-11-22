using PopPop.Data.Entities.Enums;

namespace PopPop.Models;

public sealed class CustomerOrderRowViewModel
{
    public int Id { get; init; }
    public required string OrderCode { get; init; }
    public OrderStatus Status { get; init; }
    public required string StatusText { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public PaymentStatus PaymentStatus { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public decimal TotalAmount { get; init; }
}

public sealed class CustomerOrdersViewModel
{
    public IReadOnlyList<CustomerOrderRowViewModel> Orders { get; init; } = [];
}
