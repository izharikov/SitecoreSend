namespace SitecoreSend.SDK
{
    public interface ICampaignService
    {
        Task<SendResponse<Campaign>?> GetCampaign(Guid campaignId, CancellationToken? cancellationToken = null);
    }
}