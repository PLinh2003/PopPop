namespace PopPop.Models;

public sealed class ProductVariantOptionViewModel
{
    public int Id { get; init; }
    public required string Label { get; init; }
    public required string SKU { get; init; }
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public bool IsAvailable { get; init; }
}
