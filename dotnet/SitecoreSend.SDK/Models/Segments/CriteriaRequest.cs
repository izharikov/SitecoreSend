using System.Text.Json.Serialization;

namespace SitecoreSend.SDK;

public class CriteriaRequest
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required CriteriaType Field { get; set; }

    public Guid? CustomFieldID { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required ComparerCriteria Comparer { get; set; }

    public string? Value { get; set; }
    public int? LastXMinutes { get; set; }
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DateFunction? DateFunction { get; set; }

    public Guid? OtherMailingListID { get; set; }
    public Guid? CampaignID { get; set; }
    public string? ProductCode { get; set; }
    public string? Website { get; set; }
}