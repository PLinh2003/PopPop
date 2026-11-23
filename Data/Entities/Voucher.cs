using PopPop.Data.Entities.Enums;

namespace PopPop.Data.Entities;

public class Voucher : IBaseEntity<int>
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public VoucherDiscountType DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal MinimumOrderAmount { get; set; }
    public decimal? MaximumDiscountAmount { get; set; }
    public int? MaximumUses { get; set; }
    public int UsedCount { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
