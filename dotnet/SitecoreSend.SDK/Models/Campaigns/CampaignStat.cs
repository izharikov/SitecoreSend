namespace SitecoreSend.SDK;

public class CampaignStatistics : BasePagingResponse
{
    public IList<AnalyticsItem> Analytics { get; set; } = new List<AnalyticsItem>();
}