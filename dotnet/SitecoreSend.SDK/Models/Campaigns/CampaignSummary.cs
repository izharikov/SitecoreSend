using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class CampaignSummary
{
    public Guid CampaignID { get; set; }
    public ABVersion? ABVersion { get; set; }
    public required string CampaignName { get; set; }
    public required string CampaignSubject { get; set; }
    public List<MailingListReference> MailingLists { get; set; } = new List<MailingListReference>();
    
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CampaignDeliveredOn { get; set; }
    
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? To { get; set; }
    
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset? From { get; set; }
    
    public int TotalOpens { get; set; }
    public int UniqueOpens { get; set; }
    public int TotalBounces { get; set; }
    public int TotalComplaints { get; set; }
    public int TotalForwards { get; set; }
    public int UniqueForwards { get; set; }
    public int TotalUnsubscribes { get; set; }
    public int TotalLinkClicks { get; set; }
    public int UniqueLinkClicks { get; set; }
    public int Sent { get; set; }
    public bool CampaignIsArchived { get; set; }
}