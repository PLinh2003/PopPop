using PopPop.Services.Interfaces;
using PopPop.Data.Entities;
using PopPop.Models;
using PopPop.Repositories.Interfaces;

namespace PopPop.Services.Implementations
{
    public class ProductService : Service<Product, int>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork)
            : base(productRepository, unitOfWork)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyList<ProductCardViewModel>> GetLatestAvailableAsync(
            int count,
            CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetLatestAvailableAsync(count, cancellationToken);
            return MapToProductCards(products);
        }

        public async Task<IReadOnlyList<ProductCardViewModel>> SearchAsync(
            string keyword,
            int count = 6,
            CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.SearchAsync(keyword, count, cancellationToken);
            return MapToProductCards(products);
        }

        public async Task<ProductDetailsViewModel?> GetDetailsAsync(
            int productId,
            CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetActiveDetailsAsync(productId, cancellationToken);
            if (product is null)
            {
                return null;
            }

            return new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CategoryName = product.Category?.Name,
                BrandName = product.Brand?.Name,
                ImageUrls = product.ProductImages.Select(image => image.ImageUrl).ToList(),
                Variants = product.ProductVariants.Select(variant => new ProductVariantOptionViewModel
                {
                    Id = variant.Id,
                    Label = string.IsNullOrWhiteSpace(variant.VariantLabel) ? variant.SKU : variant.VariantLabel,
                    SKU = variant.SKU,
                    Price = variant.Price,
                    StockQuantity = variant.StockQuantity,
                    IsAvailable = variant.IsActive && variant.StockQuantity > 0
                }).ToList()
            };
        }

        private static IReadOnlyList<ProductCardViewModel> MapToProductCards(IEnumerable<Product> products)
        {
            return products.Select(product =>
            {
                var variant = product.ProductVariants
                    .OrderBy(item => item.Price)
                    .ThenBy(item => item.Id)
                    .FirstOrDefault();

                return new ProductCardViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageUrl = product.ProductImages.FirstOrDefault()?.ImageUrl,
                    Price = variant?.Price ?? 0m
                };
            }).ToList();
        }
    }
} 
