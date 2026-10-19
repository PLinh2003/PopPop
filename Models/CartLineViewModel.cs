namespace PopPop.Models;

public sealed class CartLineViewModel
{
    public int Id { get; init; }
    public int ProductVariantId { get; init; }
    public required string ProductName { get; init; }
    public required string SKU { get; init; }
    public string? ImageUrl { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public int StockQuantity { get; init; }
    public decimal LineTotal => UnitPrice * Quantity;
}
