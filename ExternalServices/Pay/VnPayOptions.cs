namespace PopPop.ExternalServices.Pay;

public sealed class VnPayOptions
{
    public const string SectionName = "Payment:VnPay";

    public string PaymentUrl { get; set; } = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
    public string TmnCode { get; set; } = string.Empty;
    public string HashSecret { get; set; } = string.Empty;
    public string CallbackBaseUrl { get; set; } = string.Empty;
    public int PaymentTimeoutMinutes { get; set; } = 15;

    public bool IsConfigured =>
        !string.IsNullOrWhiteSpace(TmnCode)
        && !string.IsNullOrWhiteSpace(HashSecret)
        && (string.IsNullOrWhiteSpace(CallbackBaseUrl)
            || (Uri.TryCreate(CallbackBaseUrl, UriKind.Absolute, out var callbackUri)
                && callbackUri.Scheme == Uri.UriSchemeHttps
                && string.IsNullOrEmpty(callbackUri.UserInfo)
                && string.IsNullOrEmpty(callbackUri.Query)
                && string.IsNullOrEmpty(callbackUri.Fragment)))
        && Uri.TryCreate(PaymentUrl, UriKind.Absolute, out var paymentUri)
        && paymentUri.Scheme == Uri.UriSchemeHttps
        && PaymentTimeoutMinutes is > 0 and <= 60;
}
