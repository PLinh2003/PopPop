namespace PopPop.Data.Entities.Enums;

/// <summary>
/// Order status enum
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Order is pending confirmation
    /// </summary>
    Pending = 1,

    /// <summary>
    /// Order has been confirmed
    /// </summary>
    Confirmed = 2,

    /// <summary>
    /// Order is being processed
    /// </summary>
    Processing = 3,

    /// <summary>
    /// Order is being shipped
    /// </summary>
    Shipping = 4,

    /// <summary>
    /// Order has been completed
    /// </summary>
    Completed = 5,

    /// <summary>
    /// Order has been cancelled
    /// </summary>
    Cancelled = 6
}
