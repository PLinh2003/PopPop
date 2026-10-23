namespace PopPop.Models;

public sealed class OrderLineViewModel
{
    public required string ProductName { get; init; }
    public required string SKU { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal LineTotal { get; init; }
}
