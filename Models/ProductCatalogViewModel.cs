namespace PopPop.Models;

public sealed class ProductCategoryOptionViewModel
{
    public int Id { get; init; }
    public required string Name { get; init; }
}

public sealed class ProductCatalogViewModel
{
    public IReadOnlyList<ProductCardViewModel> Products { get; init; } = [];
    public IReadOnlyList<ProductCategoryOptionViewModel> Categories { get; init; } = [];
    public int? CategoryId { get; init; }
    public string? Search { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public decimal PriceRangeMax { get; init; }
    public string SortBy { get; init; } = "newest";
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public int TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
}
