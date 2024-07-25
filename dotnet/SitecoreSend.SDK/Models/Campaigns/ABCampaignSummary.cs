namespace SitecoreSend.SDK;

public class ABCampaignSummary
{
    public Guid? Campaign { get; set; }
    public CampaignSummary? A { get; set; }
    public CampaignSummary? B { get; set; }
}