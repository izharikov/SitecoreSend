using SitecoreSend.SDK.Tests.Http;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class SubscribersServiceTests(ITestOutputHelper testOutputHelper)
{
    // actual limits are lower, but to ensure it works I increased them
    // https://doc.sitecore.com/send/en/developers/api-documentation/api-rate-limiting.html
    private const int SecondsLimit = 15;
    
    private readonly ISubscribersService _service =
        new SubscribersService(TestsApp.ApiConfiguration,
            CustomHttpFactory.Create(testOutputHelper),
            addSubscriberClient: CustomHttpFactory.CreateRate(testOutputHelper, SecondsLimit, 7),
            addMultipleSubscribersClient: CustomHttpFactory.CreateRate(testOutputHelper, SecondsLimit, 2),
            unsubscribeFromListClient: CustomHttpFactory.CreateRate(testOutputHelper, SecondsLimit, 20),
            unsubscribeFromListAndCampaignClient: CustomHttpFactory.CreateRate(testOutputHelper, SecondsLimit, 20),
            unsubscribeFromAccountClient: CustomHttpFactory.CreateRate(testOutputHelper, SecondsLimit, 20)
        );

    [Fact]
    public async Task Subscribers_OnTestSubscriber_ShouldFindSubscriberAndUpdate()
    {
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        var email = TestsApp.Configuration.GetSection("SitecoreSend:TestExistingSubscriber").Value!;

        const string dateFieldName = "DateField";
        var subscriber = await _service.GetSubscriberByEmail(listId, email);
        Assert.True(subscriber?.Success);
        Assert.NotNull(subscriber?.Data);
        var date = (DateTimeOffset) DateTime.UtcNow.Date;
        subscriber.Data[dateFieldName] = date;
        subscriber.Data.Tags.Clear();
        subscriber.Data.Tags.Add("Tag1");
        subscriber = await _service.UpdateSubscriber(listId, subscriber.Data.ID, subscriber.Data);
        Assert.True(subscriber?.Success);
        Assert.NotNull(subscriber?.Data);
        Assert.Equal(date,
            subscriber.Data.CustomFields.FirstOrDefault(x => x.Name == dateFieldName)?.Value);
        Assert.Collection(subscriber.Data.Tags, x => Assert.Equal("Tag1", x));
        subscriber = await _service.GetSubscriberById(listId, subscriber.Data.ID);
        Assert.True(subscriber?.Success);
        Assert.NotNull(subscriber?.Data);
        Assert.Equal(email, subscriber.Data.Email);
    }

    [Fact]
    public async Task Subscribers_OnNewSingleSubscriber_ShouldSubscribeAndUnsubscribe()
    {
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        var email = TestsApp.Configuration.GetSection("SitecoreSend:NewSubscriber").Value!;

        var response = await _service.AddSubscriber(listId, new SubscriberRequest()
        {
            Email = email,
            CustomFields = [("DateField", DateTimeOffset.Now)],
        });
        Assert.NotNull(response?.Data);
        Assert.Equal(email, response.Data.Email);

        var subscribed = await _service.GetAllSubscribers(listId);
        Assert.NotNull(subscribed?.Data);
        Assert.Contains(email, subscribed.Data.Subscribers.Select(x => x.Email));

        var unsubscribeSingle = await _service.UnsubscribeFromList(listId, email);
        Assert.True(unsubscribeSingle?.Success);

        subscribed = await _service.GetAllSubscribers(listId);
        Assert.NotNull(subscribed?.Data);
        Assert.DoesNotContain(email, subscribed.Data.Subscribers.Select(x => x.Email));

        var unsubscribed = await _service.GetAllSubscribers(listId, SubscriberStatus.Unsubscribed);
        Assert.NotNull(unsubscribed?.Data);
        Assert.Contains(email, unsubscribed.Data.Subscribers.Select(x => x.Email));

        // add again 
        response = await _service.AddSubscriber(listId, new SubscriberRequest()
        {
            Email = email,
            CustomFields = [("DateField", DateTimeOffset.Now)],
        });
        Assert.NotNull(response?.Data);
        Assert.Equal(email, response.Data.Email);

        // remove subscriber
        var removeSingle = await _service.RemoveSubscriberFromList(listId, email);
        Assert.True(removeSingle?.Success);

        subscribed = await _service.GetAllSubscribers(listId);
        Assert.NotNull(subscribed?.Data);
        Assert.DoesNotContain(email, subscribed.Data.Subscribers.Select(x => x.Email));

        unsubscribed = await _service.GetAllSubscribers(listId, SubscriberStatus.Unsubscribed);
        Assert.NotNull(unsubscribed?.Data);
        Assert.DoesNotContain(email, unsubscribed.Data.Subscribers.Select(x => x.Email));

        var removed = await _service.GetAllSubscribers(listId, SubscriberStatus.Removed);
        Assert.NotNull(removed?.Data);
        Assert.Contains(email, removed.Data.Subscribers.Select(x => x.Email));
    }

    [Fact]
    public async Task Subscribers_OnNewMultipleSubscribers_ShouldSubscribeAndUnsubscribe()
    {
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        var emails = TestsApp.Configuration.GetSection("SitecoreSend:NewMultipleSubscribers").GetChildren()
            .Select(x => x.Value!).ToList();

        await Task.WhenAll(emails.Select(async x => await _service.AddSubscriber(listId, new SubscriberRequest()
        {
            Email = x,
        })));
        var addSubscribers = await _service.AddMultipleSubscribers(listId, new MultipleSubscribersRequest()
        {
            Subscribers = emails.Select(x => new SubscriberRequest()
            {
                Email = x,
            }).ToList(),
        });

        Assert.NotNull(addSubscribers?.Data);
        Assert.NotEmpty(addSubscribers.Data);

        var subscribed = await _service.GetAllSubscribers(listId);
        Assert.NotNull(subscribed?.Data);
        var subscribedEmails = subscribed.Data.Subscribers.Select(x => x.Email).ToHashSet();
        Assert.All(emails, x => Assert.Contains(x, subscribedEmails));

        // remove all subscribers
        await Task.WhenAll(emails.Select(x => _service.UnsubscribeFromList(listId, x)));

        subscribed = await _service.GetAllSubscribers(listId);
        Assert.NotNull(subscribed?.Data);
        subscribedEmails = subscribed.Data.Subscribers.Select(x => x.Email).ToHashSet();
        Assert.All(emails, x => Assert.DoesNotContain(x, subscribedEmails));

        var unsubscribed = await _service.GetAllSubscribers(listId, SubscriberStatus.Unsubscribed);
        Assert.NotNull(unsubscribed?.Data);
        var unSubscribedEmails = unsubscribed.Data.Subscribers.Select(x => x.Email).ToHashSet();
        Assert.All(emails, x => Assert.Contains(x, unSubscribedEmails));

        // subscribe again
        foreach (var email in emails)
        {
            await _service.AddSubscriber(listId, new SubscriberRequest()
            {
                Email = email,
            });
        }
        // addSubscribers = await _service.AddMultipleSubscribers(listId, new MultipleSubscribersRequest()
        // {
        //     Subscribers = emails.Select(x => new SubscriberRequest()
        //     {
        //         Email = x,
        //     }).ToList(),
        // });
        //
        // Assert.NotNull(addSubscribers?.Data);
        // Assert.NotEmpty(addSubscribers.Data);

        // remove subscribers
        var removeSubscribers = await _service.RemoveMultipleSubscribersFromList(listId, emails.ToArray());
        Assert.True(removeSubscribers?.Success);

        subscribed = await _service.GetAllSubscribers(listId);
        Assert.NotNull(subscribed?.Data);
        subscribedEmails = subscribed.Data.Subscribers.Select(x => x.Email).ToHashSet();
        Assert.All(emails, x => Assert.DoesNotContain(x, subscribedEmails));

        unsubscribed = await _service.GetAllSubscribers(listId, SubscriberStatus.Unsubscribed);
        Assert.NotNull(unsubscribed?.Data);
        unSubscribedEmails = unsubscribed.Data.Subscribers.Select(x => x.Email).ToHashSet();
        Assert.All(emails, x => Assert.DoesNotContain(x, unSubscribedEmails));

        var removed = await _service.GetAllSubscribers(listId, SubscriberStatus.Removed);
        Assert.NotNull(removed?.Data);
        var removedEmails = removed.Data.Subscribers.Select(x => x.Email).ToHashSet();
        Assert.All(emails, x => Assert.Contains(x, removedEmails));
    }

    [Fact]
    public async Task Subscribers_UnsubscribeFromAllLists_ShouldUnsubscribe()
    {
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        var emails = TestsApp.Configuration.GetSection("SitecoreSend:NewMultipleSubscribers").GetChildren()
            .Select(x => x.Value!).ToList();
        var email = emails[0];

        var response = await _service.AddSubscriber(listId, new SubscriberRequest()
        {
            Email = email,
            CustomFields = [("DateField", DateTimeOffset.Now)],
        });
        Assert.NotNull(response?.Data);
        Assert.Equal(email, response.Data.Email);

        var result = await _service.UnsubscribeFromAllLists(emails[0]);
        Assert.True(result?.Success);
    }
}