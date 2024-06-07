using System.Net.Http;

namespace SitecoreSend.SDK;

public class CampaignService : BaseApiService, ICampaignService
{
    public Task<SendResponse<CampaignListResponse>?> GetAll(int page, int pageSize,
        SortBy sortBy = SortBy.CreatedOn, SortMethod sortMethod = SortMethod.ASC,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{page}/{pageSize}");
        return Get<SendResponse<CampaignListResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<Campaign>?> Get(Guid campaignId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/view");
        return Get<SendResponse<Campaign>>(url, cancellationToken);
    }

    public CampaignService(ApiConfiguration apiConfiguration, Func<HttpClient> httpClientFactory,
        bool disposeHttpClient = false) : base(apiConfiguration, httpClientFactory, disposeHttpClient)
    {
    }
}