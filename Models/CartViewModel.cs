namespace PopPop.Models;

public sealed class CartViewModel
{
    public IReadOnlyList<CartLineViewModel> Items { get; init; } = [];
    public decimal TotalAmount => Items.Sum(item => item.LineTotal);
    public bool CanCheckout => Items.Count > 0 && Items.All(item => item.StockQuantity >= item.Quantity);
}
