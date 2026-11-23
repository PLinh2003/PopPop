using System.ComponentModel.DataAnnotations;
using PopPop.Data.Entities.Enums;

namespace PopPop.Models;

public sealed class AfterSalesListViewModel
{
    public IReadOnlyList<AfterSalesRequestRowViewModel> Requests { get; set; } = [];
}

public sealed class AfterSalesRequestRowViewModel
{
    public int Id { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public AfterSalesRequestType Type { get; set; }
    public AfterSalesRequestStatus Status { get; set; }
    public int Quantity { get; set; }
    public string Reason { get; set; } = string.Empty;
    public string? StaffNote { get; set; }
    public string? ReplacementSKU { get; set; }
    public DateTime CreatedAt { get; set; }
}

public sealed class AfterSalesCreateViewModel
{
    public int OrderId { get; set; }

    [Range(1, int.MaxValue)]
    public int OrderItemId { get; set; }

    [EnumDataType(typeof(AfterSalesRequestType))]
    public AfterSalesRequestType Type { get; set; }

    [Range(1, 1000)]
    public int Quantity { get; set; } = 1;

    [Required, StringLength(1000)]
    public string Reason { get; set; } = string.Empty;

    public string OrderCode { get; set; } = string.Empty;
    public IReadOnlyList<AfterSalesOrderItemOptionViewModel> Items { get; set; } = [];
}

public sealed class AfterSalesOrderItemOptionViewModel
{
    public int OrderItemId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public int OrderedQuantity { get; set; }
    public int WarrantyMonths { get; set; }
}
