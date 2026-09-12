namespace PopPop.Data.Entities;

/// <summary>
/// CartItem entity
/// </summary>
public class CartItem
{
    public int Id { get; set; }

    /// <summary>
    /// Cart ID (Foreign Key)
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// ProductVariant ID (Foreign Key)
    /// </summary>
    public int ProductVariantId { get; set; }

    /// <summary>
    /// Quantity in cart
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price at the time of adding to cart
    /// </summary>
    public decimal UnitPrice { get; set; }

    // Navigation properties
    public virtual Cart? Cart { get; set; }
    public virtual ProductVariant? ProductVariant { get; set; }
}
