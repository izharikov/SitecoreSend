namespace SitecoreSend.SDK.Extensions;

public static class SubscriberServiceExtensions
{
    public static async Task<SendResponse<Subscriber>?> CreateOrUpdate(this ISubscribersService subscribersService,
        Guid listId,
        SubscriberRequest subscriberRequest, CancellationToken? cancellationToken = null)
    {
        var existing =
            await subscribersService.GetSubscriberByEmail(listId, subscriberRequest.Email, cancellationToken);
        if (existing is {Success: true, Data: not null})
        {
            return await subscribersService.UpdateSubscriber(listId, existing.Data.ID, subscriberRequest,
                cancellationToken);
        }

        return await subscribersService.AddSubscriber(listId, subscriberRequest, cancellationToken);
    }

    public static async Task EnsureSubscribed(this ISubscribersService subscribersService, Guid listId, string email,
        CancellationToken? cancellationToken = null)
    {
        var existing = await subscribersService.GetSubscriberByEmail(listId, email, cancellationToken);
        if (existing is {Success: true, Data: not null})
        {
            if (existing.Data.SubscribeType == SubscriberStatus.Subscribed)
            {
                return;
            }

            await subscribersService.UpdateSubscriber(listId, existing.Data.ID, new SubscriberRequest()
            {
                Email = email,
                SubscribeType = SubscriberStatus.Subscribed,
            }, cancellationToken);
            return;
        }

        await subscribersService.AddSubscriber(listId, new SubscriberRequest()
        {
            Email = email,
            SubscribeType = SubscriberStatus.Subscribed,
        }, cancellationToken);
    }
}