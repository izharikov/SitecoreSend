namespace SitecoreSend.SDK;

/// <summary>
/// Use to fetch your subscribers, get details about your subscribers, add, update, remove subscribers, and unsubscribe subscribers from email lists or campaigns.
/// </summary>
public interface ISubscribersService
{
    /// <summary>
    /// Retrieves a list of subscribers in a specific email list in your Sitecore Send account. You can filter the list by status. Because this call can return a large number of results, you can add paging information as input.
    /// </summary>
    /// <param name="listId">The ID of the email list containing the subscribers.</param>
    /// <param name="status">Specifies the type of subscriber statistics results to return.</param>
    /// <param name="page">The page of subscriber statistics results to return.</param>
    /// <param name="pageSize">The page size of subscriber statistics results to return.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<SubscribersResponse>?> GetAllSubscribers(Guid listId,
        SubscriberStatus status = SubscriberStatus.Subscribed,
        int? page = null, int? pageSize = null,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a subscriber by email address in a specific email list in your account. The response returns detailed information such as ID, name, date created, date unsubscribed, status, and custom fields.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the subscriber.</param>
    /// <param name="email">The email address of the subscriber.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Subscriber>?> GetSubscriberByEmail(Guid listId, string email,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Retrieves the details about a subscriber by ID in a specific email list in your account. The response returns detailed information such as ID, name, date created, date unsubscribed, status, and custom fields.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the subscriber.</param>
    /// <param name="subscriberId">The ID of the subscriber.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Subscriber>?> GetSubscriberById(Guid listId, Guid subscriberId,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Adds a new subscriber to a specific email list in your Sitecore Send account. If the subscriber with the specified email address already exists in the list, the list is instead updated.<br/><br/>
    /// <i>Note</i>. When you add a single subscriber to an email list manually or through the API, the subscriber is added regardless of whether the subscriber has previously been unsubscribed.
    /// </summary>
    /// <param name="listId">The ID of the email list where you want to add a new subscriber.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Subscriber>?> AddSubscriber(Guid listId, SubscriberRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Adds multiple subscribers to a specific email list in your Sitecore Send account in a single request. Our API supports requests with up to 1000 records per batch. <br/> <br/>
    /// If some subscribers with their specified email addresses already exist in the list, an update is done instead. If you add a subscriber with an invalid email address, it is ignored.
    /// </summary>
    /// <param name="listId">The ID of the email list where you want to add multiple subscribers.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<IList<Subscriber>>?> AddMultipleSubscribers(Guid listId,
        MultipleSubscribersRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Updates the details of a subscriber in a specific email list in your Sitecore Send account.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the subscriber.</param>
    /// <param name="subscriberId">The ID of the subscriber that you want to update.</param>
    /// <param name="request">Request body</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<Subscriber>?> UpdateSubscriber(Guid listId, Guid subscriberId, SubscriberRequest request,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Unsubscribes a specific subscriber from all email lists they belong to in your Sitecore Send account.
    /// </summary>
    /// <param name="email">The email address of the subscriber.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> UnsubscribeFromAllLists(string email, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Unsubscribes a subscriber from a specific email list in your Sitecore Send account.
    /// </summary>
    /// <param name="listId">The ID of the email list from which you want to unsubscribe a subscriber.</param>
    /// <param name="email">The email address of the subscriber.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> UnsubscribeFromList(Guid listId, string email, CancellationToken? cancellationToken = null);

    /// <summary>
    /// Unsubscribes a subscriber from a specific email list and campaign. This call considers the Unsubscribe settings in your Sitecore Send account where you can specify whether to remove a subscriber from all other email lists.
    /// </summary>
    /// <param name="listId">The ID of the email list from which you want to unsubscribe a subscriber.</param>
    /// <param name="campaignId">The ID of the campaign sent to the specific email list.</param>
    /// <param name="email">The email address of the subscriber.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> UnsubscribeFromListAndCampaign(Guid listId, Guid campaignId, string email,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Removes a subscriber from a specific email list permanently without moving the subscriber to the suppression list. The status of the subscriber is Archived.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the subscriber you want to remove.</param>
    /// <param name="email">The email address of the subscriber.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse?> RemoveSubscriberFromList(Guid listId, string email,
        CancellationToken? cancellationToken = null);

    /// <summary>
    /// Removes multiple subscribers from a specific email list permanently without moving the subscribers to the suppression list. The status of each removed subscriber is Archived. Any invalid email addresses specified in the list are ignored.
    /// </summary>
    /// <param name="listId">The ID of the email list that contains the subscribers you want to remove.</param>
    /// <param name="emails">A list of subscriber email addresses that you want to remove from the email list.</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns></returns>
    Task<SendResponse<RemoveMultipleSubscribersResponse>?> RemoveMultipleSubscribersFromList(Guid listId,
        string[] emails,
        CancellationToken? cancellationToken = null);
}