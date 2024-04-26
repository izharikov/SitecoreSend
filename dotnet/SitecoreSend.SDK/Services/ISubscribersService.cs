namespace SitecoreSend.SDK
{
    // TODO: add documentation
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

        // TODO: fix response
        /*
         *{
  "Code": 0,
  "Error": null,
  "Context": {
    "EmailsIgnored": 0,
    "EmailsProcessed": 3
  }
}
         * 
         */
        Task<SendResponse?> RemoveMultipleSubscribersFromList(Guid listId, string[] emails,
            CancellationToken? cancellationToken = null);
    }
}