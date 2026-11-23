using System.ComponentModel.DataAnnotations;
using PopPop.Data.Entities.Enums;

namespace PopPop.Models;

public sealed class VoucherManagementListViewModel
{
    public IReadOnlyList<VoucherManagementRowViewModel> Vouchers { get; set; } = [];
}

public sealed class VoucherManagementRowViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public VoucherDiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public int UsedCount { get; set; }
    public int? MaximumUses { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
}

public sealed class VoucherManagementFormViewModel
{
    public int? Id { get; set; }

    [Required, StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [EnumDataType(typeof(VoucherDiscountType))]
    public VoucherDiscountType DiscountType { get; set; }

    [Range(typeof(decimal), "0.01", "1000000000")]
    public decimal DiscountValue { get; set; }

    [Range(typeof(decimal), "0", "1000000000")]
    public decimal MinimumOrderAmount { get; set; }

    [Range(typeof(decimal), "0.01", "1000000000")]
    public decimal? MaximumDiscountAmount { get; set; }

    [Range(1, int.MaxValue)]
    public int? MaximumUses { get; set; }

    public DateTime StartsAt { get; set; } = DateTime.Now;
    public DateTime ExpiresAt { get; set; } = DateTime.Now.AddDays(30);
    public bool IsActive { get; set; } = true;
}
