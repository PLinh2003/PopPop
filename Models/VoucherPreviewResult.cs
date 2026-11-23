namespace PopPop.Models;

public sealed record VoucherPreviewResult(bool IsValid, decimal DiscountAmount, string Message);
