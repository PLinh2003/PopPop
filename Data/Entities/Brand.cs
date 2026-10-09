namespace PopPop.Data.Entities;

/// <summary>
/// Brand entity
/// </summary>
public class Brand
{
    public int Id { get; set; }

    /// <summary>
    /// Brand name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Brand description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Is brand active
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Created timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Last updated timestamp
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
