namespace PopPop.Models;

public sealed class ProductDetailsViewModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public string? CategoryName { get; init; }
    public string? BrandName { get; init; }
    public IReadOnlyList<string> ImageUrls { get; init; } = [];
    public IReadOnlyList<ProductVariantOptionViewModel> Variants { get; init; } = [];
    public bool HasAvailableVariants => Variants.Any(variant => variant.IsAvailable);
}
