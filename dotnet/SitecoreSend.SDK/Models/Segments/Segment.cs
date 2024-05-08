using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK.Models.Segments;

// TODO: complete
public class Segment
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public int MatchType { get; set; }
    public List<object>? Criteria { get; set; }
    public string? CreatedBy { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CreatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset UpdatedOn { get; set; }
    public int FetchType { get; set; }
    public int FetchValue { get; set; }
    public string? Description { get; set; }
    public object? SortBy { get; set; }
}