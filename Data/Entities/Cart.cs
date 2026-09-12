namespace PopPop.Data.Entities;

/// <summary>
/// Cart entity
/// </summary>
public class Cart
{
    public int Id { get; set; }

    /// <summary>
    /// User ID (Foreign Key)
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Created timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Last updated timestamp
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public virtual User? User { get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
