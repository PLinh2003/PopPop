namespace PopPop.Models;

public sealed class VnPayReturnViewModel
{
    public string OrderReference { get; init; } = string.Empty;
    public bool IsSignatureValid { get; init; }
    public bool IsTransactionSuccessful { get; init; }
    public string Message { get; init; } = string.Empty;
}
