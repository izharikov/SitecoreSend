using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class AnalyticsItem
{
    public string Context { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? Timestamp { get; set; }

    public int TotalCount { get; set; }
    public int UniqueCount { get; set; }
}