using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class CampaignListItem
{
    public required string ID { get; set; }
    public required string Name { get; set; }
    public string? Subject { get; set; }
    public string? SiteName { get; set; }
    public string? ConfirmationTo { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CreatedOn { get; set; }
    public int? ABHoursToTest { get; set; }
    public ABCampaignType? ABCampaignType { get; set; }
    public ABVersion? ABWinner { get; set; }
    public ABWinnerSelectionType? ABWinnerSelectionType { get; set; }
    public CampaignStatus Status { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? DeliveredOn { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? ScheduledFor { get; set; }
    public string? ScheduledForTimezone { get; set; }
    public List<CampaignMailingList>? MailingLists { get; set; }
    public int TotalSent { get; set; }
    public int TotalOpens { get; set; }
    public int UniqueOpens { get; set; }
    public int TotalBounces { get; set; }
    public int TotalForwards { get; set; }
    public int UniqueForwards { get; set; }
    public int TotalLinkClicks { get; set; }
    public int UniqueLinkClicks { get; set; }
    public int RecipientsCount { get; set; }
    public bool IsTransactional { get; set; }
    public int TotalComplaints { get; set; }
    public int TotalUnsubscribes { get; set; }
}