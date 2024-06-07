using System.Net.Http;

namespace SitecoreSend.SDK;

public class SubscribersService : BaseApiService, ISubscribersService
{
    private readonly SubscribersWrapper? _limiterWrapper;

    public Task<SendResponse<SubscribersResponse>?> GetAll(Guid listId,
        SubscriberStatus status = SubscriberStatus.Subscribed,
        int? page = null, int? pageSize = null,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/subscribers/{status.ToString()}", "Page", page, "PageSize", pageSize);
        return Get<SendResponse<SubscribersResponse>>(url, cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> GetByEmail(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/view", "Email", email);
        return Get<SendResponse<Subscriber>>(url, cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> Get(Guid listId, Guid subscriberId,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/find/{subscriberId}");
        return Get<SendResponse<Subscriber>>(url, cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> Add(Guid listId, SubscriberRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/subscribe");
        return Wrap(_limiterWrapper?.AddSubscriber,
            () => Post<SendResponse<Subscriber>>(url, request, cancellationToken), cancellationToken);
    }

    public Task<SendResponse<IList<Subscriber>>?> AddMultiple(Guid listId,
        MultipleSubscribersRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/subscribe_many");
        return Wrap(_limiterWrapper?.AddMultipleSubscribers,
            () => Post<SendResponse<IList<Subscriber>>>(url, request, cancellationToken), cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> Update(Guid listId, Guid subscriberId,
        SubscriberRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/update/{subscriberId}");
        return Post<SendResponse<Subscriber>>(url, request, cancellationToken);
    }

    public Task<SendResponse?> UnsubscribeFromAll(string email, CancellationToken? cancellationToken = null)
    {
        var url = Url("subscribers/unsubscribe");
        return Wrap(_limiterWrapper?.UnsubscribeFromAllLists, () => Post<SendResponse>(url, new
        {
            Email = email,
        }, cancellationToken), cancellationToken);
    }

    public Task<SendResponse?> Unsubscribe(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/unsubscribe");
        return Wrap(_limiterWrapper?.UnsubscribeFromList, () => Post<SendResponse>(url, new
        {
            Email = email,
        }, cancellationToken), cancellationToken);
    }

    public Task<SendResponse?> UnsubscribeFromListAndCampaign(Guid listId, Guid campaignId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/{campaignId}/unsubscribe");
        return Wrap(_limiterWrapper?.UnsubscribeFromListAndCampaign, () => Post<SendResponse>(url, new
        {
            Email = email,
        }, cancellationToken), cancellationToken);
    }

    public Task<SendResponse?> Remove(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/remove");
        return Post<SendResponse>(url, new
        {
            Email = email,
        }, cancellationToken);
    }

    public Task<SendResponse<RemoveMultipleSubscribersResponse>?> RemoveMultiple(Guid listId,
        string[] emails,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/remove_many");
        return Post<SendResponse<RemoveMultipleSubscribersResponse>>(url, new
        {
            Emails = string.Join(",", emails),
        }, cancellationToken);
    }

    private static Task<TResult?> Wrap<TResult>(Wrapper<TResult>? func,
        Func<Task<TResult?>> apiCall, CancellationToken? cancellationToken)
    {
        return func == null ? apiCall() : func(_ => apiCall(), cancellationToken ?? CancellationToken.None);
    }

    public SubscribersService(ApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory,
        SubscribersWrapper? limiterWrapper = null) : base(
        apiConfiguration, httpClientFactory)
    {
        _limiterWrapper = limiterWrapper;
    }
}