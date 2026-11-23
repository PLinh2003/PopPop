using PopPop.Data.Entities.Enums;

namespace PopPop.Models;

public sealed class AfterSalesManagementListViewModel
{
    public AfterSalesRequestStatus? Status { get; set; }
    public IReadOnlyList<AfterSalesManagementRowViewModel> Requests { get; set; } = [];
}

public sealed class AfterSalesManagementRowViewModel
{
    public int Id { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public AfterSalesRequestType Type { get; set; }
    public AfterSalesRequestStatus Status { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class AfterSalesManagementDetailsViewModel
{
    public int Id { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public AfterSalesRequestType Type { get; set; }
    public AfterSalesRequestStatus Status { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? StaffNote { get; set; }
    public string? ReplacementSKU { get; set; }
    public DateTime CreatedAt { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public IReadOnlyList<AfterSalesReplacementOptionViewModel> ReplacementVariants { get; set; } = [];
    public IReadOnlyList<AfterSalesRequestStatus> AvailableStatuses { get; set; } = [];
}

public sealed class AfterSalesReplacementOptionViewModel
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
}
