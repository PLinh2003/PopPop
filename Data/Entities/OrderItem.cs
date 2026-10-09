namespace PopPop.Data.Entities;

/// <summary>
/// OrderItem entity - represents a line item in an order
/// </summary>
public class OrderItem
{
    public int Id { get; set; }

    /// <summary>
    /// Order ID (Foreign Key)
    /// </summary>
    public int OrderId { get; set; }

    /// <summary>
    /// ProductVariant ID (Foreign Key)
    /// </summary>
    public int ProductVariantId { get; set; }

    /// <summary>
    /// Product name snapshot
    /// </summary>
    public required string ProductName { get; set; }

    /// <summary>
    /// SKU snapshot
    /// </summary>
    public required string SKU { get; set; }

    /// <summary>
    /// Unit price snapshot
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Quantity ordered
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Total price for this line item
    /// </summary>
    public decimal TotalPrice { get; set; }

    // Navigation properties
    public virtual Order? Order { get; set; }
    public virtual ProductVariant? ProductVariant { get; set; }
}
