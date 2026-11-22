namespace PopPop.ExternalServices.Pay;

public interface IVnPayClient
{
    bool IsConfigured { get; }
    int PaymentTimeoutMinutes { get; }
    string CreatePaymentUrl(string transactionReference, decimal amount, string clientIpAddress, string callbackBaseUrl, DateTime createdAtUtc, DateTime expiresAtUtc);
    bool ValidateSignature(IReadOnlyDictionary<string, string> parameters);
}
