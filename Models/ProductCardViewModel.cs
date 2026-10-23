namespace PopPop.Models;

public sealed class ProductCardViewModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? ImageUrl { get; init; }
    public decimal Price { get; init; }
}
