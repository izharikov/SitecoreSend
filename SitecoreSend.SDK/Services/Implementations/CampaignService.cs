using System.Net.Http;

namespace SitecoreSend.SDK
{
    public class CampaignService : BaseApiService, ICampaignService
    {
        public CampaignService(ApiConfiguration apiConfiguration, HttpClient httpClient) : base(apiConfiguration,
            httpClient)
        {
        }

        public CampaignService(ApiConfiguration apiConfiguration) : base(apiConfiguration)
        {
        }

        public Task<SendResponse<Campaign>?> GetCampaign(Guid campaignId, CancellationToken? cancellationToken = null)
        {
            var url = Url($"campaigns/{campaignId}/view");
            return Get<SendResponse<Campaign>>(url, cancellationToken);
        }
    }
}