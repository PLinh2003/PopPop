namespace PopPop.Data.Entities;

/// <summary>
/// ProductImage entity
/// </summary>
public class ProductImage
{
    public int Id { get; set; }

    /// <summary>
    /// Product ID (Foreign Key)
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Image URL
    /// </summary>
    public required string ImageUrl { get; set; }

    /// <summary>
    /// Is this the primary image
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// Sort order
    /// </summary>
    public int SortOrder { get; set; }

    // Navigation property
    public virtual Product? Product { get; set; }
}
