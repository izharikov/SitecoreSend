using System.Net.Http;

namespace SitecoreSend.SDK;

public class SubscribersService : BaseApiService, ISubscribersService
{
    private readonly HttpClient _getClient;
    private readonly HttpClient _addSubscriberClient;
    private readonly HttpClient _addMultipleSubscribersClient;
    private readonly HttpClient _unsubscribeFromListClient;
    private readonly HttpClient _unsubscribeFromListAndCampaignClient;
    private readonly HttpClient _unsubscribeFromAccountClient;

    public SubscribersService(ApiConfiguration apiConfiguration, HttpClient getClient, HttpClient addSubscriberClient,
        HttpClient addMultipleSubscribersClient, HttpClient unsubscribeFromListClient,
        HttpClient unsubscribeFromListAndCampaignClient,
        HttpClient unsubscribeFromAccountClient) : base(apiConfiguration, getClient)
    {
        _getClient = getClient;
        _addSubscriberClient = addSubscriberClient;
        _addMultipleSubscribersClient = addMultipleSubscribersClient;
        _unsubscribeFromListClient = unsubscribeFromListClient;
        _unsubscribeFromListAndCampaignClient = unsubscribeFromListAndCampaignClient;
        _unsubscribeFromAccountClient = unsubscribeFromAccountClient;
        _getClient.BaseAddress
            = _addSubscriberClient.BaseAddress
                = _addMultipleSubscribersClient.BaseAddress
                    = _unsubscribeFromListClient.BaseAddress
                        = _unsubscribeFromListAndCampaignClient.BaseAddress
                            = _unsubscribeFromAccountClient.BaseAddress
                                = new Uri(apiConfiguration.BaseUri);
    }

    public SubscribersService(ApiConfiguration apiConfiguration, HttpClient defaultClient) : this(apiConfiguration,
        defaultClient, defaultClient, defaultClient, defaultClient, defaultClient, defaultClient)
    {
    }

    public SubscribersService(ApiConfiguration apiConfiguration) : this(apiConfiguration,
        CreateDefaultClient(apiConfiguration))
    {
    }

    public Task<SendResponse<SubscribersResponse>?> GetAllSubscribers(Guid listId,
        SubscriberStatus status = SubscriberStatus.Subscribed,
        int? page = null, int? pageSize = null,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"lists/{listId}/subscribers/{status.ToString()}", "Page", page, "PageSize", pageSize);
        return Get<SendResponse<SubscribersResponse>>(_getClient, url, cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> GetSubscriberByEmail(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/view", "Email", email);
        return Get<SendResponse<Subscriber>>(_getClient, url, cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> GetSubscriberById(Guid listId, Guid subscriberId,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/find/{subscriberId}");
        return Get<SendResponse<Subscriber>>(_getClient, url, cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> AddSubscriber(Guid listId, SubscriberRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/subscribe");
        return Post<SendResponse<Subscriber>>(_addSubscriberClient, url, request, cancellationToken);
    }

    public Task<SendResponse<IList<Subscriber>>?> AddMultipleSubscribers(Guid listId,
        MultipleSubscribersRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/subscribe_many");
        return Post<SendResponse<IList<Subscriber>>>(_addMultipleSubscribersClient, url, request, cancellationToken);
    }

    public Task<SendResponse<Subscriber>?> UpdateSubscriber(Guid listId, Guid subscriberId,
        SubscriberRequest request,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/update/{subscriberId}");
        return Post<SendResponse<Subscriber>>(_addSubscriberClient, url, request, cancellationToken);
    }

    public Task<SendResponse?> UnsubscribeFromAllLists(string email, CancellationToken? cancellationToken = null)
    {
        var url = Url("subscribers/unsubscribe");
        return Post<SendResponse>(_unsubscribeFromAccountClient, url, new
        {
            Email = email,
        }, cancellationToken);
    }

    public Task<SendResponse?> UnsubscribeFromList(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/unsubscribe");
        return Post<SendResponse>(_unsubscribeFromListClient, url, new
        {
            Email = email,
        }, cancellationToken);
    }

    public Task<SendResponse?> UnsubscribeFromListAndCampaign(Guid listId, Guid campaignId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/{campaignId}/unsubscribe");
        return Post<SendResponse>(_unsubscribeFromListAndCampaignClient, url, new
        {
            Email = email,
        }, cancellationToken);
    }

    public Task<SendResponse?> RemoveSubscriberFromList(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/remove");
        return Post<SendResponse>(_unsubscribeFromListClient, url, new
        {
            Email = email,
        }, cancellationToken);
    }

    public Task<SendResponse<RemoveMultipleSubscribersResponse>?> RemoveMultipleSubscribersFromList(Guid listId,
        string[] emails,
        CancellationToken? cancellationToken = null)
    {
        var url = Url($"subscribers/{listId}/remove_many");
        return Post<SendResponse<RemoveMultipleSubscribersResponse>>(_unsubscribeFromListClient, url, new
        {
            Emails = string.Join(",", emails),
        }, cancellationToken);
    }
}