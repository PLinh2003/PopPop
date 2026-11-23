using System.ComponentModel.DataAnnotations;

namespace PopPop.Models;

public sealed class ProductManagementListViewModel
{
    public string? Search { get; set; }
    public IReadOnlyList<ProductManagementRowViewModel> Products { get; set; } = [];
}

public sealed class ProductManagementRowViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public int WarrantyMonths { get; set; }
    public bool IsActive { get; set; }
    public int VariantCount { get; set; }
    public int StockQuantity { get; set; }
}

public sealed class ProductManagementFormViewModel
{
    public int? Id { get; set; }

    [Required, StringLength(256)]
    public string Name { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }

    [Range(1, int.MaxValue)]
    public int BrandId { get; set; }

    [Range(0, 120)]
    public int WarrantyMonths { get; set; }

    [StringLength(500)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;
    [StringLength(100)]
    public string? VariantSKU { get; set; }

    [StringLength(100)]
    public string? VariantLabel { get; set; }
    public decimal VariantPrice { get; set; }
    public IReadOnlyList<ProductOptionViewModel> Categories { get; set; } = [];
    public IReadOnlyList<ProductOptionViewModel> Brands { get; set; } = [];
    public IReadOnlyList<ProductVariantManagementViewModel> Variants { get; set; } = [];
}

public sealed class ProductOptionViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public sealed class ProductVariantManagementViewModel
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string? Label { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
}
