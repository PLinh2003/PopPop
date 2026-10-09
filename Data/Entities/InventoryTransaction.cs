using PopPop.Data.Entities.Enums;

namespace PopPop.Data.Entities;

/// <summary>
/// InventoryTransaction entity - tracks inventory movements
/// </summary>
public class InventoryTransaction
{
    public int Id { get; set; }

    /// <summary>
    /// ProductVariant ID (Foreign Key)
    /// </summary>
    public int ProductVariantId { get; set; }

    /// <summary>
    /// Transaction type
    /// </summary>
    public InventoryTransactionType Type { get; set; }

    /// <summary>
    /// Transaction quantity (+/-)
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Transaction notes
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Created timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public virtual ProductVariant? ProductVariant { get; set; }
}
