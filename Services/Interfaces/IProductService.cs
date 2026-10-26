using PopPop.Data.Entities;
using PopPop.Models;

namespace PopPop.Services.Interfaces
{
    public interface IProductService : IService<Product, int>
    {
            Task<IReadOnlyList<ProductCardViewModel>> GetLatestAvailableAsync(
                int count,
                CancellationToken cancellationToken = default);
            Task<IReadOnlyList<ProductCardViewModel>> SearchAsync(
                string keyword,
                int count = 6,
                CancellationToken cancellationToken = default);
            Task<ProductDetailsViewModel?> GetDetailsAsync(int productId, CancellationToken cancellationToken = default);
        }
    }
