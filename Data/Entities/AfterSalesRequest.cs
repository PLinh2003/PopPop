using PopPop.Data.Entities.Enums;

namespace PopPop.Data.Entities;

public class AfterSalesRequest : IBaseEntity<int>
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int OrderItemId { get; set; }
    public required string UserId { get; set; }
    public AfterSalesRequestType Type { get; set; }
    public AfterSalesRequestStatus Status { get; set; } = AfterSalesRequestStatus.Pending;
    public int Quantity { get; set; }
    public required string Reason { get; set; }
    public string? StaffNote { get; set; }
    public int? ReplacementProductVariantId { get; set; }
    public string? ReplacementSKU { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public virtual Order? Order { get; set; }
    public virtual OrderItem? OrderItem { get; set; }
    public virtual User? User { get; set; }
    public virtual ProductVariant? ReplacementProductVariant { get; set; }
}
