using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopPop.Data;
using PopPop.Data.Entities;
using PopPop.Models;

namespace PopPop.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public sealed class ProductsController : Controller
{
    private readonly IDbContextFactory<PopPopDbContext> _contextFactory;

    public ProductsController(IDbContextFactory<PopPopDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? search, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var query = context.Products.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(product => product.Name.Contains(term)
                || product.ProductVariants.Any(variant => variant.SKU.Contains(term)));
        }

        var products = await query.OrderBy(product => product.Name)
            .Select(product => new ProductManagementRowViewModel
            {
                Id = product.Id,
                Name = product.Name,
                CategoryName = product.Category!.Name,
                BrandName = product.Brand!.Name,
                WarrantyMonths = product.WarrantyMonths,
                IsActive = product.IsActive,
                VariantCount = product.ProductVariants.Count,
                StockQuantity = product.ProductVariants.Sum(variant => variant.StockQuantity)
            }).ToListAsync(cancellationToken);

        return View(new ProductManagementListViewModel { Search = search, Products = products });
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new ProductManagementFormViewModel();
        await LoadOptionsAsync(model, cancellationToken);
        return View("Form", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductManagementFormViewModel model, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(model.VariantSKU) || model.VariantPrice <= 0)
        {
            ModelState.AddModelError(nameof(model.VariantSKU), "Nhập SKU và giá hợp lệ cho biến thể đầu tiên.");
        }

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var variantSku = model.VariantSKU?.Trim();
        if (await context.ProductVariants.AnyAsync(variant => variant.SKU == variantSku, cancellationToken))
        {
            ModelState.AddModelError(nameof(model.VariantSKU), "SKU đã tồn tại.");
        }

        if (!await context.Categories.AnyAsync(category => category.Id == model.CategoryId, cancellationToken)
            || !await context.Brands.AnyAsync(brand => brand.Id == model.BrandId, cancellationToken))
        {
            ModelState.AddModelError(string.Empty, "Danh mục hoặc thương hiệu không hợp lệ.");
        }

        if (!ModelState.IsValid)
        {
            await LoadOptionsAsync(model, cancellationToken);
            return View("Form", model);
        }

        var product = new Product
        {
            Name = model.Name.Trim(),
            Description = model.Description?.Trim(),
            CategoryId = model.CategoryId,
            BrandId = model.BrandId,
            WarrantyMonths = model.WarrantyMonths,
            IsActive = model.IsActive,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        product.ProductVariants.Add(new ProductVariant
        {
            SKU = model.VariantSKU!.Trim(),
            VariantLabel = model.VariantLabel?.Trim(),
            Price = model.VariantPrice,
            StockQuantity = 0,
            IsActive = true
        });
        if (!string.IsNullOrWhiteSpace(model.ImageUrl))
        {
            product.ProductImages.Add(new ProductImage { ImageUrl = model.ImageUrl.Trim(), IsPrimary = true, SortOrder = 0 });
        }

        context.Products.Add(product);
        await context.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã tạo sản phẩm. Hãy nhập tồn kho ở mục quản lý kho.";
        return RedirectToAction(nameof(Edit), new { id = product.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var product = await context.Products.AsNoTracking()
            .Include(item => item.ProductVariants)
            .Include(item => item.ProductImages)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (product is null) return NotFound();

        var model = new ProductManagementFormViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,
            BrandId = product.BrandId,
            WarrantyMonths = product.WarrantyMonths,
            IsActive = product.IsActive,
            ImageUrl = product.ProductImages.FirstOrDefault(image => image.IsPrimary)?.ImageUrl
                ?? product.ProductImages.FirstOrDefault()?.ImageUrl,
            Variants = product.ProductVariants.OrderBy(variant => variant.Id)
                .Select(variant => new ProductVariantManagementViewModel
                {
                    Id = variant.Id,
                    SKU = variant.SKU,
                    Label = variant.VariantLabel,
                    Price = variant.Price,
                    StockQuantity = variant.StockQuantity,
                    IsActive = variant.IsActive
                }).ToList()
        };
        await LoadOptionsAsync(model, cancellationToken);
        return View("Form", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductManagementFormViewModel model, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var product = await context.Products.Include(item => item.ProductImages)
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
        if (product is null) return NotFound();
        model.Id = id;

        if (!await context.Categories.AnyAsync(category => category.Id == model.CategoryId, cancellationToken)
            || !await context.Brands.AnyAsync(brand => brand.Id == model.BrandId, cancellationToken))
        {
            ModelState.AddModelError(string.Empty, "Danh mục hoặc thương hiệu không hợp lệ.");
        }
        if (!ModelState.IsValid)
        {
            var variants = await context.ProductVariants.AsNoTracking().Where(variant => variant.ProductId == id)
                .OrderBy(variant => variant.Id).Select(variant => new ProductVariantManagementViewModel
                {
                    Id = variant.Id,
                    SKU = variant.SKU,
                    Label = variant.VariantLabel,
                    Price = variant.Price,
                    StockQuantity = variant.StockQuantity,
                    IsActive = variant.IsActive
                }).ToListAsync(cancellationToken);
            model.Variants = variants;
            await LoadOptionsAsync(model, cancellationToken);
            return View("Form", model);
        }

        product.Name = model.Name.Trim();
        product.Description = model.Description?.Trim();
        product.CategoryId = model.CategoryId;
        product.BrandId = model.BrandId;
        product.WarrantyMonths = model.WarrantyMonths;
        product.IsActive = model.IsActive;
        product.UpdatedAt = DateTime.UtcNow;
        if (!string.IsNullOrWhiteSpace(model.ImageUrl))
        {
            var primaryImage = product.ProductImages.FirstOrDefault(image => image.IsPrimary)
                ?? product.ProductImages.FirstOrDefault();
            if (primaryImage is null)
            {
                product.ProductImages.Add(new ProductImage { ImageUrl = model.ImageUrl.Trim(), IsPrimary = true, SortOrder = 0 });
            }
            else
            {
                primaryImage.ImageUrl = model.ImageUrl.Trim();
                primaryImage.IsPrimary = true;
            }
        }
        await context.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã cập nhật sản phẩm.";
        return RedirectToAction(nameof(Edit), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddVariant(int productId, string sku, string? label, decimal price, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var product = await context.Products.FirstOrDefaultAsync(item => item.Id == productId, cancellationToken);
        if (product is null) return NotFound();
        if (string.IsNullOrWhiteSpace(sku) || sku.Length > 100 || (label?.Length ?? 0) > 100 || price <= 0)
        {
            TempData["ErrorMessage"] = "Nhập SKU (tối đa 100 ký tự) và giá lớn hơn 0.";
            return RedirectToAction(nameof(Edit), new { id = productId });
        }
        if (await context.ProductVariants.AnyAsync(item => item.SKU == sku.Trim(), cancellationToken))
        {
            TempData["ErrorMessage"] = "SKU đã tồn tại.";
            return RedirectToAction(nameof(Edit), new { id = productId });
        }
        context.ProductVariants.Add(new ProductVariant
        {
            ProductId = productId,
            SKU = sku.Trim(),
            VariantLabel = label?.Trim(),
            Price = price,
            StockQuantity = 0,
            IsActive = true
        });
        await context.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã thêm biến thể với tồn kho ban đầu bằng 0.";
        return RedirectToAction(nameof(Edit), new { id = productId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateVariant(int productId, int variantId, string sku, string? label, decimal price, bool isActive, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        var variant = await context.ProductVariants.FirstOrDefaultAsync(item => item.Id == variantId && item.ProductId == productId, cancellationToken);
        if (variant is null) return NotFound();
        if (string.IsNullOrWhiteSpace(sku) || sku.Length > 100 || (label?.Length ?? 0) > 100 || price <= 0
            || await context.ProductVariants.AnyAsync(item => item.Id != variantId && item.SKU == sku.Trim(), cancellationToken))
        {
            TempData["ErrorMessage"] = "SKU không hợp lệ/trùng lặp hoặc giá không hợp lệ.";
            return RedirectToAction(nameof(Edit), new { id = productId });
        }
        variant.SKU = sku.Trim();
        variant.VariantLabel = label?.Trim();
        variant.Price = price;
        variant.IsActive = isActive;
        await context.SaveChangesAsync(cancellationToken);
        TempData["SuccessMessage"] = "Đã cập nhật biến thể.";
        return RedirectToAction(nameof(Edit), new { id = productId });
    }

    private async Task LoadOptionsAsync(ProductManagementFormViewModel model, CancellationToken cancellationToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);
        model.Categories = await context.Categories.AsNoTracking().Where(item => item.IsActive)
            .OrderBy(item => item.Name).Select(item => new ProductOptionViewModel { Id = item.Id, Name = item.Name }).ToListAsync(cancellationToken);
        model.Brands = await context.Brands.AsNoTracking().Where(item => item.IsActive)
            .OrderBy(item => item.Name).Select(item => new ProductOptionViewModel { Id = item.Id, Name = item.Name }).ToListAsync(cancellationToken);
    }
}
