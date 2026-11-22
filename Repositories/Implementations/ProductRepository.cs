using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities;
using PopPop.Repositories.Interfaces;

namespace PopPop.Repositories.Implementations;

public sealed class ProductRepository : Repository<Product, int>, IProductRepository
{
    public ProductRepository(PopPopDbContext context)
        : base(context)
    {
    }

    public async Task<IReadOnlyList<Product>> GetLatestAvailableAsync(
        int count,
        CancellationToken cancellationToken = default)
    {
        if (count <= 0)
        {
            return [];
        }

        return await DbSet
            .AsNoTracking()
            .AsSplitQuery()
            .Where(product => product.IsActive &&
                product.ProductVariants.Any(variant => variant.IsActive && variant.StockQuantity > 0))
            .OrderByDescending(product => product.CreatedAt)
            .ThenByDescending(product => product.Id)
            .Take(count)
            .Include(product => product.ProductImages
                .OrderByDescending(image => image.IsPrimary)
                .ThenBy(image => image.SortOrder))
            .Include(product => product.ProductVariants
                .Where(variant => variant.IsActive && variant.StockQuantity > 0))
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> SearchAsync(
        string keyword,
        int count = 6,
        CancellationToken cancellationToken = default)
    {
        if (count <= 0 || string.IsNullOrWhiteSpace(keyword))
        {
            return [];
        }

        var normalizedKeyword = keyword.Trim();

        return await DbSet
            .AsNoTracking()
            .AsSplitQuery()
            .Where(product => product.IsActive &&
                product.Name.Contains(normalizedKeyword) &&
                product.ProductVariants.Any(variant => variant.IsActive && variant.StockQuantity > 0))
            .OrderBy(product => product.Name)
            .ThenByDescending(product => product.CreatedAt)
            .Take(count)
            .Include(product => product.ProductImages
                .OrderByDescending(image => image.IsPrimary)
                .ThenBy(image => image.SortOrder))
            .Include(product => product.ProductVariants
                .Where(variant => variant.IsActive && variant.StockQuantity > 0))
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Product> Products, int TotalCount)> GetCatalogPageAsync(
        int pageNumber,
        int pageSize,
        int? categoryId,
        string? search,
        decimal? minPrice,
        decimal? maxPrice,
        string sortBy,
        CancellationToken cancellationToken = default)
    {
        var query = DbSet
            .AsNoTracking()
            .Where(product => product.IsActive
                && product.ProductVariants.Any(variant => variant.IsActive && variant.StockQuantity > 0));

        if (categoryId.HasValue)
        {
            query = query.Where(product => product.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedSearch = search.Trim();
            query = query.Where(product => product.Name.Contains(normalizedSearch));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(product => product.ProductVariants
                .Where(variant => variant.IsActive && variant.StockQuantity > 0)
                .Min(variant => (decimal?)variant.Price) >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(product => product.ProductVariants
                .Where(variant => variant.IsActive && variant.StockQuantity > 0)
                .Min(variant => (decimal?)variant.Price) <= maxPrice.Value);
        }

        var totalCount = await query.CountAsync(cancellationToken);
        var orderedQuery = sortBy switch
        {
            "price-asc" => query
                .OrderBy(product => product.ProductVariants
                    .Where(variant => variant.IsActive && variant.StockQuantity > 0)
                    .Min(variant => (decimal?)variant.Price))
                .ThenBy(product => product.Id),
            "price-desc" => query
                .OrderByDescending(product => product.ProductVariants
                    .Where(variant => variant.IsActive && variant.StockQuantity > 0)
                    .Min(variant => (decimal?)variant.Price))
                .ThenByDescending(product => product.Id),
            "name" => query.OrderBy(product => product.Name).ThenBy(product => product.Id),
            _ => query.OrderByDescending(product => product.CreatedAt).ThenByDescending(product => product.Id)
        };

        var products = await orderedQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsSplitQuery()
            .Include(product => product.ProductImages
                .OrderByDescending(image => image.IsPrimary)
                .ThenBy(image => image.SortOrder))
            .Include(product => product.ProductVariants
                .Where(variant => variant.IsActive && variant.StockQuantity > 0)
                .OrderBy(variant => variant.Price)
                .ThenBy(variant => variant.Id))
            .ToListAsync(cancellationToken);

        return (products, totalCount);
    }

    public async Task<IReadOnlyList<Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Categories
            .AsNoTracking()
            .Where(category => category.IsActive)
            .OrderBy(category => category.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetCatalogMaxPriceAsync(CancellationToken cancellationToken = default)
    {
        return await Context.ProductVariants
            .AsNoTracking()
            .Where(variant => variant.IsActive
                && variant.StockQuantity > 0
                && variant.Product != null
                && variant.Product.IsActive)
            .Select(variant => (decimal?)variant.Price)
            .MaxAsync(cancellationToken) ?? 0m;
    }

    public async Task<ProductVariant?> GetAvailableVariantAsync(
        int variantId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<ProductVariant>()
            .AsNoTracking()
            .FirstOrDefaultAsync(variant =>
                variant.Id == variantId &&
                variant.IsActive &&
                variant.StockQuantity > 0 &&
                variant.Product != null &&
                variant.Product.IsActive,
                cancellationToken);
    }

    public async Task<Product?> GetActiveDetailsAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .AsNoTracking()
            .AsSplitQuery()
            .Where(product => product.Id == productId && product.IsActive)
            .Include(product => product.Category)
            .Include(product => product.Brand)
            .Include(product => product.ProductImages
                .OrderByDescending(image => image.IsPrimary)
                .ThenBy(image => image.SortOrder))
            .Include(product => product.ProductVariants
                .OrderBy(variant => variant.VariantLabel)
                .ThenBy(variant => variant.SKU))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
