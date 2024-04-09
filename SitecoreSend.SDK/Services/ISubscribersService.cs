namespace SitecoreSend.SDK
{
    public interface ISubscribersService
    {
        Task<SendResponse<SubscribersResponse>?> GetAllSubscribers(Guid listId, SubscriberStatus status = SubscriberStatus.Subscribed,
            int? page = null, int? pageSize = null,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<Subscriber>?> GetSubscriberByEmail(Guid listId, string email,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<Subscriber>?> GetSubscriberById(Guid listId, Guid subscriberId,
            CancellationToken? cancellationToken = null);

        Task<SendResponse<Subscriber>?> AddSubscriber(Guid listId, SubscriberRequest request,
            CancellationToken? cancellationToken = null);

        /// <summary>
        /// Note: works only for non-unsubscribed customers
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<SendResponse<IList<Subscriber>>?> AddMultipleSubscribers(Guid listId,
            MultipleSubscribersRequest request, CancellationToken? cancellationToken = null);

        Task<SendResponse<Subscriber>?> UpdateSubscriber(Guid listId, Guid subscriberId, SubscriberRequest request,
            CancellationToken? cancellationToken = null);

        Task<SendResponse?> UnsubscribeFromAllLists(string email, CancellationToken? cancellationToken = null);

        Task<SendResponse?> UnsubscribeFromList(Guid listId, string email, CancellationToken? cancellationToken = null);

        Task<SendResponse?> UnsubscribeFromListAndCampaign(Guid listId, Guid campaignId, string email,
            CancellationToken? cancellationToken = null);

        Task<SendResponse?> RemoveSubscriberFromList(Guid listId, string email,
            CancellationToken? cancellationToken = null);

        Task<SendResponse?> RemoveMultipleSubscribersFromList(Guid listId, string[] emails,
            CancellationToken? cancellationToken = null);
    }
}