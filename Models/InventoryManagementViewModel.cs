using PopPop.Data.Entities.Enums;

namespace PopPop.Models;

public sealed class InventoryManagementViewModel
{
    public string? Search { get; set; }
    public IReadOnlyList<InventoryVariantRowViewModel> Variants { get; set; } = [];
    public IReadOnlyList<InventoryTransactionRowViewModel> Transactions { get; set; } = [];
}

public sealed class InventoryVariantRowViewModel
{
    public int Id { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string? Label { get; set; }
    public int StockQuantity { get; set; }
    public bool IsActive { get; set; }
}

public sealed class InventoryTransactionRowViewModel
{
    public DateTime CreatedAt { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public InventoryTransactionType Type { get; set; }
    public int Quantity { get; set; }
    public string? Note { get; set; }
}
