namespace PopPop.Models;

public sealed class OrderDailySummary
{
    public DateTime Date { get; init; }
    public int OrderCount { get; init; }
    public decimal CompletedRevenue { get; init; }
}
