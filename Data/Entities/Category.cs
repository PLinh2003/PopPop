namespace PopPop.Data.Entities;

/// <summary>
/// Category entity
/// </summary>
public class Category
{
    public int Id { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Category description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Is category active
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
