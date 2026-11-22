namespace PopPop.Models;

public sealed record CheckoutOrderResult(int OrderId, string? PaymentUrl);
