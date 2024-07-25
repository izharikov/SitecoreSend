using System.Net.Http;

namespace SitecoreSend.SDK;

public class SegmentsService : BaseApiService, ISegmentsService
{
    public Task<SendResponse<SegmentsResponse>?> GetAll(Guid listId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments");
        return Get<SendResponse<SegmentsResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<Segment>?> Get(Guid listId, int segmentId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments/{segmentId}/details");
        return Get<SendResponse<Segment>>(url, cancellationToken);
    }

    public Task<SendResponse<SubscribersResponse>?> GetSubscribers(Guid listId, int segmentId, int? page = null,
        int? pageSize = null,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments/{segmentId}/members", "Page", page, "PageSize", pageSize);
        return Get<SendResponse<SubscribersResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<int?>?> Create(Guid listId, SegmentRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments/create");
        return Post<SendResponse<int?>>(url, request, cancellationToken);
    }

    public Task<SendResponse?> Update(Guid listId, int segmentId, SegmentRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments/{segmentId}/update");
        return Post<SendResponse>(url, request, cancellationToken);
    }

    public Task<SendResponse<int?>?> AddCriteria(Guid listId, int segmentId, CriteriaRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments/{segmentId}/criteria/add");
        return Post<SendResponse<int?>>(url, request, cancellationToken);
    }

    public Task<SendResponse?> UpdateCriteria(Guid listId, int segmentId, int criteriaId, CriteriaRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments/{segmentId}/criteria/{criteriaId}/update");
        return Post<SendResponse>(url, request, cancellationToken);
    }

    public Task<SendResponse?> Delete(Guid listId, int segmentId, CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/segments/{segmentId}/delete");
        return Delete<SendResponse>(url, cancellationToken);
    }

    public SegmentsService(ApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory) : base(
        apiConfiguration, httpClientFactory)
    {
    }
}