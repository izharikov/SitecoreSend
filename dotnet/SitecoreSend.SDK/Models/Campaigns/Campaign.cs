using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

using System;
using System.Collections.Generic;

public class Campaign
{
    public Guid ID { get; set; }
    public string? Name { get; set; }
    public string? Subject { get; set; }
    public string? WebLocation { get; set; }
    public string? HTMLContent { get; set; }
    public string? PlainContent { get; set; }
    public Sender? Sender { get; set; }
    public Sender? ReplyToEmail { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CreatedOn { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset UpdatedOn { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? DeliveredOn { get; set; }
    public DateTimeOffset? ScheduledFor { get; set; }
    public string? TimeZone { get; set; }
    public FormatType FormatType { get; set; }
    public ABCampaignData? ABCampaignData { get; set; }
    public List<MailingListReference> MailingLists { get; set; } = [];
    public string? ConfirmationTo { get; set; }
    public CampaignStatus Status { get; set; }
    public bool IsTransactional { get; set; }
}