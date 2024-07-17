using System.Collections.Specialized;
using System.Text.Json.Serialization;

namespace SitecoreSend.SDK;

using System.Collections.Generic;

public class CampaignRequest
{
    public required string Name { get; set; }
    public required string Subject { get; set; }
    public required string SenderEmail { get; set; }
    public string? ReplyToEmail { get; set; }
    public string? ConfirmationToEmail { get; set; }
    public string? HTMLContent { get; set; }
    public string? WebLocation { get; set; }
    public List<MailingListReference> MailingLists { get; set; } = new List<MailingListReference>();
    public bool IsAB { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ABCampaignType? ABCampaignType { get; set; }
    public string? SenderEmailB { get; set; }
    public string? HTMLContentB { get; set; }
    public string? WebLocationB { get; set; }
    public int? HoursToTest { get; set; }
    public int? ListPercentage { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ABWinnerSelectionType? ABWinnerSelectionType { get; set; }
    public bool TrackInGoogleAnalytics { get; set; }
}
