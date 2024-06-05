using System.Net.Http;

namespace SitecoreSend.SDK;

public class CampaignService : BaseApiService, ICampaignService
{


    public Task<SendResponse<CampaignListResponse>?> GetAllCampaigns(int page, int pageSize, SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{page}/{pageSize}");
        return Get<SendResponse<CampaignListResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<Campaign>?> GetCampaign(Guid campaignId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/view");
        return Get<SendResponse<Campaign>>(url, cancellationToken);
    }

    public CampaignService(ApiConfiguration apiConfiguration, Func<HttpClient?> httpClientFactory) : base(apiConfiguration, httpClientFactory)
    {
    }

    public CampaignService(ApiConfiguration apiConfiguration) : base(apiConfiguration)
    {
    }
}