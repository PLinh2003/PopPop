using PopPop.Data.Entities.Enums;

namespace PopPop.Data.Entities;

/// <summary>
/// Order entity
/// </summary>
public class Order
{
    public int Id { get; set; }

    /// <summary>
    /// User ID (Foreign Key)
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Order code - unique identifier
    /// </summary>
    public required string OrderCode { get; set; }

    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    /// <summary>
    /// Total amount
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Shipping fee
    /// </summary>
    public decimal ShippingFee { get; set; }

    /// <summary>
    /// Order notes
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Recipient name
    /// </summary>
    public string? RecipientName { get; set; }

    /// <summary>
    /// Recipient phone number
    /// </summary>
    public string? RecipientPhone { get; set; }

    /// <summary>
    /// Shipping address
    /// </summary>
    public string? ShippingAddress { get; set; }

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
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
