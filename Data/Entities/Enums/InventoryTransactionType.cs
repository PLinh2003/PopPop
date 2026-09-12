namespace PopPop.Data.Entities.Enums;

/// <summary>
/// Inventory transaction type enum
/// </summary>
public enum InventoryTransactionType
{
    /// <summary>
    /// Import inventory
    /// </summary>
    Import = 1,

    /// <summary>
    /// Sale transaction
    /// </summary>
    Sale = 2,

    /// <summary>
    /// Adjustment increase
    /// </summary>
    AdjustmentIncrease = 3,

    /// <summary>
    /// Adjustment decrease
    /// </summary>
    AdjustmentDecrease = 4
}
