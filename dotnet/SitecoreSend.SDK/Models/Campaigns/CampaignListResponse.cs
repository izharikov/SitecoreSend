namespace SitecoreSend.SDK;

public class CampaignListResponse : BasePagingResponse
{
    public IList<CampaignListItem> Campaigns { get; set; } = new List<CampaignListItem>();
}