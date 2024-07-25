using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class Criteria
{
    public int ID { get; set; }
    public int SegmentID { get; set; }
    public CriteriaType Field { get; set; }
    public Guid? CustomFieldID { get; set; }
    public ComparerCriteria Comparer { get; set; }
    public string? Value { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? DateFrom { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? DateTo { get; set; }

    public int? LastXMinutes { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DateFunction? DateFunction { get; set; }

    public Guid? OtherMailingListID { get; set; }
    public Guid? CampaignID { get; set; }
    public string? ProductCode { get; set; }
    public string? Website { get; set; }
}