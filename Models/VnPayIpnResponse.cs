using System.Text.Json.Serialization;

namespace PopPop.Models;

public sealed record VnPayIpnResponse(
    [property: JsonPropertyName("RspCode")] string ResponseCode,
    [property: JsonPropertyName("Message")] string Message);
