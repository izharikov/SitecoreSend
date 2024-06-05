using System.Text.Json.Serialization;

namespace SitecoreSend.SDK;

public class SendResponse<TResponse> : SendResponse
{
    [JsonPropertyName("Context")]
    public TResponse? Data { get; set; }
}

public class SendResponse
{
    public int Code { get; set; }
    public string? Error { get; set; }
    public bool Success => Code == 0 && string.IsNullOrEmpty(Error);
    public RateLimitDetails? RateLimitDetails { get; set; }
}