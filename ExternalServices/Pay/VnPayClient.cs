using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace PopPop.ExternalServices.Pay;

public sealed class VnPayClient : IVnPayClient
{
    private static readonly TimeSpan VietnamOffset = TimeSpan.FromHours(7);
    private readonly VnPayOptions _options;

    public VnPayClient(IOptions<VnPayOptions> options)
    {
        _options = options.Value;
    }

    public bool IsConfigured => _options.IsConfigured;

    public int PaymentTimeoutMinutes => _options.PaymentTimeoutMinutes;

    public string CreatePaymentUrl(
        string transactionReference,
        decimal amount,
        string clientIpAddress,
        string callbackBaseUrl,
        DateTime createdAtUtc,
        DateTime expiresAtUtc)
    {
        if (!IsConfigured)
        {
            throw new InvalidOperationException("VNPay chưa được cấu hình. Vui lòng cấu hình thông tin Sandbox trước khi thanh toán.");
        }

        if (string.IsNullOrWhiteSpace(transactionReference) || amount <= 0)
        {
            throw new ArgumentException("Thông tin giao dịch không hợp lệ.");
        }

        if (!Uri.TryCreate(callbackBaseUrl, UriKind.Absolute, out var callbackUri)
            || callbackUri.Scheme != Uri.UriSchemeHttps
            || !string.IsNullOrEmpty(callbackUri.UserInfo)
            || !string.IsNullOrEmpty(callbackUri.Query)
            || !string.IsNullOrEmpty(callbackUri.Fragment))
        {
            throw new InvalidOperationException("VNPay cần địa chỉ HTTPS hợp lệ để nhận kết quả thanh toán.");
        }

        callbackBaseUrl = callbackUri.GetLeftPart(UriPartial.Authority) + callbackUri.AbsolutePath.TrimEnd('/');
        var parameters = new SortedDictionary<string, string>(StringComparer.Ordinal)
        {
            ["vnp_Version"] = "2.1.0",
            ["vnp_Command"] = "pay",
            ["vnp_TmnCode"] = _options.TmnCode.Trim(),
            ["vnp_Amount"] = checked((long)(amount * 100m)).ToString(CultureInfo.InvariantCulture),
            ["vnp_CurrCode"] = "VND",
            ["vnp_TxnRef"] = transactionReference,
            ["vnp_OrderInfo"] = $"Thanh toan don hang {transactionReference}",
            ["vnp_OrderType"] = "other",
            ["vnp_Locale"] = "vn",
            ["vnp_ReturnUrl"] = $"{callbackBaseUrl}/Orders/VnPayReturn",
            ["vnp_IpAddr"] = string.IsNullOrWhiteSpace(clientIpAddress) ? "127.0.0.1" : clientIpAddress,
            ["vnp_CreateDate"] = FormatVietnamTime(createdAtUtc),
            ["vnp_ExpireDate"] = FormatVietnamTime(expiresAtUtc)
        };

        var query = BuildQuery(parameters);
        var signature = ComputeSignature(query, _options.HashSecret);
        return $"{_options.PaymentUrl}?{query}&vnp_SecureHash={signature}";
    }

    public bool ValidateSignature(IReadOnlyDictionary<string, string> parameters)
    {
        if (string.IsNullOrWhiteSpace(_options.HashSecret)
            || !parameters.TryGetValue("vnp_SecureHash", out var receivedHash)
            || string.IsNullOrWhiteSpace(receivedHash))
        {
            return false;
        }

        var signedParameters = new SortedDictionary<string, string>(StringComparer.Ordinal);
        foreach (var parameter in parameters)
        {
            if (parameter.Key is "vnp_SecureHash" or "vnp_SecureHashType")
            {
                continue;
            }

            signedParameters[parameter.Key] = parameter.Value;
        }

        var query = BuildQuery(signedParameters);
        var expectedHash = ComputeSignature(query, _options.HashSecret);
        try
        {
            return CryptographicOperations.FixedTimeEquals(
                Convert.FromHexString(expectedHash),
                Convert.FromHexString(receivedHash));
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static string FormatVietnamTime(DateTime utcDateTime)
    {
        var utc = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
        return new DateTimeOffset(utc).ToOffset(VietnamOffset).ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
    }

    private static string BuildQuery(IEnumerable<KeyValuePair<string, string>> parameters)
    {
        return string.Join("&", parameters.Select(parameter =>
            $"{WebUtility.UrlEncode(parameter.Key)}={WebUtility.UrlEncode(parameter.Value)}"));
    }

    private static string ComputeSignature(string data, string secret)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        var message = Encoding.UTF8.GetBytes(data);
        return Convert.ToHexString(HMACSHA512.HashData(key, message));
    }
}
