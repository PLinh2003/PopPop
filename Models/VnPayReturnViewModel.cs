namespace PopPop.Models;

public sealed class VnPayReturnViewModel
{
    public string OrderReference { get; init; } = string.Empty;
    public bool IsSignatureValid { get; init; }
    public bool IsTransactionSuccessful { get; init; }
    public bool IsLocalCallback { get; init; }
    public bool IsReturnProcessed { get; init; }
    public string Message { get; init; } = string.Empty;
}
