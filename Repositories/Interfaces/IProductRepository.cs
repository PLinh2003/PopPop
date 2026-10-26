using PopPop.Data.Entities;

namespace PopPop.Repositories.Interfaces;

public interface IProductRepository : IRepository<Product, int>
{
    Task<IReadOnlyList<Product>> GetLatestAvailableAsync(int count, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> SearchAsync(string keyword, int count = 6, CancellationToken cancellationToken = default);
    Task<Product?> GetActiveDetailsAsync(int productId, CancellationToken cancellationToken = default);
    Task<ProductVariant?> GetAvailableVariantAsync(int variantId, CancellationToken cancellationToken = default);
}
