using System.Text.Json.Serialization;

namespace SitecoreSend.SDK;

public class EmptySegmentRequest
{
    public required string Name { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MatchType MatchType { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public SegmentFetchType FetchType { get; set; }

    public int? FetchValue { get; set; }
}