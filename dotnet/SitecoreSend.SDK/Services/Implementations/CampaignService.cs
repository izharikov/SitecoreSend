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

    public Task<SendResponse<IList<Sender>>?> GetAllSenders(CancellationToken? cancellationToken = null)
    {
        var url = Url("senders/find_all");
        return Get<SendResponse<IList<Sender>>>(url, cancellationToken);
    }

    public Task<SendResponse<Sender>?> GetSender(string email, CancellationToken? cancellationToken = null)
    {
        var url = Url("senders/find_one", "Email", email);
        return Get<SendResponse<Sender>>(url, cancellationToken);
    }

    public Task<SendResponse<CampaignResponse>?> CreateDraft(CampaignRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url("campaigns/create");
        return Post<SendResponse<CampaignResponse>>(url, request, cancellationToken);
    }

    public Task<SendResponse<Campaign>?> Clone(Guid campaignId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/clone");
        return Post<SendResponse<Campaign>>(url, null, cancellationToken);
    }

    public Task<SendResponse?> Delete(Guid campaignId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/delete");
        return Delete<SendResponse>(url, cancellationToken);
    }

    public Task<SendResponse?> UpdateDraft(Guid campaignId, CampaignRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/update");
        return Post<SendResponse>(url, request, cancellationToken);
    }

    public Task<SendResponse?> SendTest(Guid campaignId, IList<string> emails,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/send_test");
        return Post<SendResponse>(url, new {TestEmails = emails}, cancellationToken);
    }

    public Task<SendResponse?> Send(Guid campaignId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/send");
        return Post<SendResponse>(url, null, cancellationToken);
    }

    public Task<SendResponse?> Schedule(Guid campaignId, DateTimeOffset date, TimeZoneInfo? timeZoneInfo = null,
        string? format = "dd-MM-yyyy HH:mm",
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/schedule");
        return Post<SendResponse>(url, new
        {
            DateTime = date.ToString(format),
            Timezone = timeZoneInfo?.ToString(),
        }, cancellationToken);
    }

    public Task<SendResponse?> Unschedule(Guid campaignId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/unschedule");
        return Post<SendResponse>(url, null, cancellationToken);
    }

    public Task<SendResponse<CampaignStatistics>?> GetStatistics(Guid campaignId, ActivityType type,
        DateTimeOffset? date = null,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/stats/{type.ToString()}", "date", date?.ToString("YYYY/MM/DD"));
        return Get<SendResponse<CampaignStatistics>>(url, cancellationToken);
    }

    public Task<SendResponse<CampaignStatistics>?> GetStatistics(Guid campaignId, ActivityType type, int page,
        int pageSize, DateTimeOffset? from = null,
        DateTimeOffset? to = null, CancellationToken? cancellationToken = null)
    {
        const string format = "dd-MM-yyyy";
        var url = Url($"campaigns/{campaignId}/stats/{type.ToString()}", "Page", page, "PageSize", pageSize, "From",
            from?.ToString(format), "To", to?.ToString(format));
        return Get<SendResponse<CampaignStatistics>>(url, cancellationToken);
    }

    public Task<SendResponse<CampaignSummary>?> GetSummary(Guid campaignId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/view_summary");
        return Get<SendResponse<CampaignSummary>>(url, cancellationToken);
    }

    public Task<SendResponse<CampaignStatistics>?> GetActivityByLocation(Guid campaignId,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/stats/countries");
        return Get<SendResponse<CampaignStatistics>>(url, cancellationToken);
    }

    public Task<SendResponse<CampaignStatistics>?> GetLinkActivity(Guid campaignId,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/stats/links");
        return Get<SendResponse<CampaignStatistics>>(url, cancellationToken);
    }

    public Task<SendResponse<ABCampaignSummary>?> GetABCampaignSummary(Guid campaignId,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"campaigns/{campaignId}/view_ab_summary");
        return Get<SendResponse<ABCampaignSummary>>(url, cancellationToken);
    }

    public CampaignService(ApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory) : base(
        apiConfiguration, httpClientFactory)
    {
    }
}