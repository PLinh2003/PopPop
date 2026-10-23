using PopPop.Data.Entities.Enums;

namespace PopPop.Models;

public sealed class OrderDetailsViewModel
{
    public required string OrderCode { get; init; }
    public OrderStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
    public required string RecipientName { get; init; }
    public required string RecipientPhone { get; init; }
    public required string ShippingAddress { get; init; }
    public string? Note { get; init; }
    public IReadOnlyList<OrderLineViewModel> Items { get; init; } = [];
    public decimal ShippingFee { get; init; }
    public decimal TotalAmount { get; init; }
}
