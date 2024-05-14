using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class MailingList
{
    public Guid ID { get; set; }
    public required string Name { get; set; }
    public int ActiveMemberCount { get; set; }
    public int BouncedMemberCount { get; set; }
    public int RemovedMemberCount { get; set; }
    public int UnsubscribedMemberCount { get; set; }
    public MailingListStatus Status { get; set; }
    public IList<CustomFieldDefinition> CustomFieldsDefinition { get; set; } = [];
    public string? CreatedBy { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CreatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset UpdatedOn { get; set; }
    public ImportOperation? ImportOperation { get; set; }
}