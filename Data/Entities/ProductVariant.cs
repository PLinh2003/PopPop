namespace PopPop.Data.Entities;

/// <summary>
/// ProductVariant entity - represents a specific variant of a product
/// </summary>
public class ProductVariant
{
    public int Id { get; set; }

    /// <summary>
    /// Product ID (Foreign Key)
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// SKU (Stock Keeping Unit) - must be unique
    /// </summary>
    public required string SKU { get; set; }

    /// <summary>
    /// Variant price
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Stock quantity
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Is variant active
    /// </summary>
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public virtual Product? Product { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
}
