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

    public Task<ProductVariant?> GetAvailableVariantAsync(
        int variantId,
        CancellationToken cancellationToken = default)
    {
        return Context.Set<ProductVariant>()
            .AsNoTracking()
            .FirstOrDefaultAsync(variant =>
                variant.Id == variantId &&
                variant.IsActive &&
                variant.StockQuantity > 0 &&
                variant.Product != null &&
                variant.Product.IsActive,
                cancellationToken);
    }

    public Task<Product?> GetActiveDetailsAsync(int productId, CancellationToken cancellationToken = default)
    {
        return DbSet
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
