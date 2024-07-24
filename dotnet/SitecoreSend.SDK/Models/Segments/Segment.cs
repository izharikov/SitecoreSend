using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class Segment
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public MatchType MatchType { get; set; }
    public List<Criteria> Criteria { get; set; } = [];
    public string? CreatedBy { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset UpdatedOn { get; set; }

    public SegmentFetchType FetchType { get; set; }
    public int? FetchValue { get; set; }
    public string? Description { get; set; }
}