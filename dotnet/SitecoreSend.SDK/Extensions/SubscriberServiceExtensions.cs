namespace SitecoreSend.SDK.Extensions;

public static class SubscriberServiceExtensions
{
    public static async Task<SendResponse<Subscriber>?> CreateOrUpdate(this ISubscribersService subscribersService,
        Guid listId,
    SubscriberRequest subscriberRequest, CancellationToken? cancellationToken = null)
    {
        return (await subscribersService.CreateOrUpdateDetailed(listId, subscriberRequest, cancellationToken)).Item1;
    }

    public static async Task<(SendResponse<Subscriber>?, ServiceOperation)> CreateOrUpdateDetailed(
        this ISubscribersService subscribersService,
        Guid listId,
        SubscriberRequest subscriberRequest, CancellationToken? cancellationToken = null)
    {
        var existing =
            await subscribersService.GetSubscriberByEmail(listId, subscriberRequest.Email, cancellationToken);
        if (existing is {Success: true, Data: not null})
        {
            return (await subscribersService.UpdateSubscriber(listId, existing.Data.ID, subscriberRequest,
                cancellationToken), ServiceOperation.Update);
        }

        return (await subscribersService.AddSubscriber(listId, subscriberRequest, cancellationToken), ServiceOperation.Add);
    }

    public static async Task<SendResponse<Subscriber>?> EnsureSubscribed(
        this ISubscribersService subscribersService, Guid listId, string email, bool subscribeIfNotExists = true,
        CancellationToken? cancellationToken = null)
    {
        return (await subscribersService.EnsureSubscribedDetailed(listId, email, subscribeIfNotExists, cancellationToken)).Item1;
    }
    

    public static async Task<(SendResponse<Subscriber>?, ServiceOperation)> EnsureSubscribedDetailed(
        this ISubscribersService subscribersService, Guid listId, string email, bool subscribeIfNotExists = true,
        CancellationToken? cancellationToken = null)
    {
        var existing = await subscribersService.GetSubscriberByEmail(listId, email, cancellationToken);
        if (existing is {Success: true, Data: not null})
        {
            if (existing.Data.SubscribeType == SubscriberStatus.Subscribed)
            {
                return (existing, ServiceOperation.None);
            }

            return (await subscribersService.UpdateSubscriber(listId, existing.Data.ID, new SubscriberRequest()
            {
                Email = email,
                SubscribeType = SubscriberStatus.Subscribed,
            }, cancellationToken), ServiceOperation.Update);
        }

        if (!subscribeIfNotExists)
        {
            return (null, ServiceOperation.None);
        }
        
        return (await subscribersService.AddSubscriber(listId, new SubscriberRequest()
        {
            Email = email,
            SubscribeType = SubscriberStatus.Subscribed,
        }, cancellationToken), ServiceOperation.Add);
    }
}