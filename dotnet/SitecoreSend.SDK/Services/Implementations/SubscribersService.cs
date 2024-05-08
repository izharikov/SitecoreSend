using System.Net.Http;

namespace SitecoreSend.SDK;

public class SubscribersService : BaseApiService, ISubscribersService
{
    public SubscribersService(ApiConfiguration apiConfiguration, HttpClient httpClient) : base(apiConfiguration,
        httpClient)
    {
        }

    public SubscribersService(ApiConfiguration apiConfiguration) : base(apiConfiguration)
    {
        }

    public Task<SendResponse<SubscribersResponse>?> GetAllSubscribers(Guid listId,
        SubscriberStatus status = SubscriberStatus.Subscribed,
        int? page = null, int? pageSize = null,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"lists/{listId}/subscribers/{status.ToString()}", "Page", page, "PageSize", pageSize);
            return Get<SendResponse<SubscribersResponse>>(url, cancellationToken);
        }

    public Task<SendResponse<Subscriber>?> GetSubscriberByEmail(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/view", "Email", email);
            return Get<SendResponse<Subscriber>>(url, cancellationToken);
        }

    public Task<SendResponse<Subscriber>?> GetSubscriberById(Guid listId, Guid subscriberId,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/find/{subscriberId}");
            return Get<SendResponse<Subscriber>>(url, cancellationToken);
        }

    public Task<SendResponse<Subscriber>?> AddSubscriber(Guid listId, SubscriberRequest request,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/subscribe");
            return Post<SendResponse<Subscriber>>(url, request, cancellationToken);
        }

    public Task<SendResponse<IList<Subscriber>>?> AddMultipleSubscribers(Guid listId,
        MultipleSubscribersRequest request,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/subscribe_many");
            return Post<SendResponse<IList<Subscriber>>>(url, request, cancellationToken);
        }

    public Task<SendResponse<Subscriber>?> UpdateSubscriber(Guid listId, Guid subscriberId,
        SubscriberRequest request,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/update/{subscriberId}");
            return Post<SendResponse<Subscriber>>(url, request, cancellationToken);
        }

    public Task<SendResponse?> UnsubscribeFromAllLists(string email, CancellationToken? cancellationToken = null)
    {
            var url = Url("subscribers/unsubscribe");
            return Post<SendResponse>(url, new
            {
                Email = email,
            }, cancellationToken);
        }

    public Task<SendResponse?> UnsubscribeFromList(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/unsubscribe");
            return Post<SendResponse>(url, new
            {
                Email = email,
            }, cancellationToken);
        }

    public Task<SendResponse?> UnsubscribeFromListAndCampaign(Guid listId, Guid campaignId, string email,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/{campaignId}/unsubscribe");
            return Post<SendResponse>(url, new
            {
                Email = email,
            }, cancellationToken);
        }

    public Task<SendResponse?> RemoveSubscriberFromList(Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/remove");
            return Post<SendResponse>(url, new
            {
                Email = email,
            }, cancellationToken);
        }

    public Task<SendResponse<RemoveMultipleSubscribersResponse>?> RemoveMultipleSubscribersFromList(Guid listId, string[] emails,
        CancellationToken? cancellationToken = null)
    {
            var url = Url($"subscribers/{listId}/remove_many");
            return Post<SendResponse<RemoveMultipleSubscribersResponse>>(url, new
            {
                Emails = string.Join(",", emails),
            }, cancellationToken);
        }
}