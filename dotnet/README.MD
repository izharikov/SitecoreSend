# Sitecore Send SDK (Unofficial)
[![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/izharikov/SitecoreSend/dotnet.yml)](https://github.com/izharikov/SitecoreSend/actions/workflows/dotnet.yml)

[![NuGet Version](https://img.shields.io/nuget/v/SitecoreSend.SDK)](https://www.nuget.org/packages/SitecoreSend.SDK)
[![NuGet Downloads](https://img.shields.io/nuget/dt/SitecoreSend.SDK)](https://www.nuget.org/packages/SitecoreSend.SDK)

Provides .NET wrapper around [Sitecore Send API](https://doc.sitecore.com/send/en/developers/api-documentation/index-en.html)

## Usage

```csharp
var apiConfiguration = new ApiConfiguration
{
    ApiKey = "[YOUR API KEY]",
};

// or any other service
ISendClient send = new SendClient(apiConfiguration);

var response = await send.Lists.GetAll();

// ensure response is not null and Success is true
if (response is {Success: true})
{
    IList<MailingList>? allLists = response.Data?.MailingLists;
    // handle allLists
}
```

Configuration with DI:

```csharp
var apiConfiguration = new ApiConfiguration
{
    ApiKey = "[YOUR API KEY]",
};
serviceCollection.AddHttpClient("SitecoreSendClient",
    (client) => { client.BaseAddress = new Uri(apiConfiguration.BaseUri); });
serviceCollection.AddSingleton<ISendClient>(provider =>
{
    var factory = provider.GetRequiredService<IHttpClientFactory>();
    return new SendClient(apiConfiguration,
        () => factory.CreateClient("SitecoreSendClient"));
});
```

## API Rate limit
Sitecore Send comes with [API rate limits](https://doc.sitecore.com/send/en/developers/api-documentation/api-rate-limiting.html) for subscribe/unsubscribe endpoints.

### Handle within SDK
To check if rate limit occurred the following can be used:
```csharp
var response = _send.Subscribers.Add(...);
// option 1:
if (response?.Code == KnownErrors.RATE_LIMITING) 
{
    ...
}
// option 2:
if (response?.RateLimitDetails != null)
{
    ...
}
```

Current library supports handling of them with 'Wrapping' limited methods with configured policies.

### Polly
The following [Polly](https://github.com/App-vNext/Polly) policies configuration can be used: [link](./SitecoreSend.SDK.Tests/Limiter/SendRateLimiterPolicy.cs).

Currently, applies only for `SubscribersService`:
```csharp
new SendClient(
    apiConfiguration,
    httpClientFactory, 
    new RateLimiterConfiguration()
    {
        Subscribers = new SubscribersWrapper()
        {
            AddSubscriber = SendRateLimits.AddSubscriber.ExecuteAsync,
            AddMultipleSubscribers = SendRateLimits.AddMultipleSubscribers.ExecuteAsync,
            UnsubscribeFromAllLists = SendRateLimits.UnsubscribeFromAllLists.ExecuteAsync,
            UnsubscribeFromList = SendRateLimits.UnsubscribeFromList.ExecuteAsync,
            UnsubscribeFromListAndCampaign = SendRateLimits.UnsubscribeFromListAndCampaign.ExecuteAsync,
        },
    }
);
```
### Avoid rate limit issues
Whenever possible use `Get`, `Update` methods of `ISendClient.Subscribers`.

E.g. if you need to ensure user exists in the list, use extension methods:

```csharp
// check if subscriber exists => then update, if not => create
_send.Subscribers.CreateOrUpdate(listId, ...);

// check if user is subscribed to the list, if subscribeIfNotExists == true => subscribe to list
_send.Subscribers.EnsureSubscribed(listId, email, subscribeIfNotExists);
```

## Integration tests
### Overview
Integration tests are located in `SitecoreSend.SDK.Tests` project.

### Running locally
Create file `appsettings.local.json` in `SitecoreSend.SDK.Tests` project.

Fill it with the following content:
```json5
{
  "SitecoreSend": {
    "ApiKey": "[YOUR API KEY]",
    "TestListId": "[TEST LIST ID]"
  },
  "UnitTests": {
    "HideSecrets": false // specify, if hide api secrets and emails in test outputs 
  }
}
```

## Contribute / Bug / Help
You can contact me via email or in Sitecore Slack: `zharikovigor97@gmail.com`. Or please open an [issue](https://github.com/izharikov/SitecoreSend/issues/new).

## Roadmap
### Documented API
- [x] Mailing list service
- [x] Subscribers service
- [ ] Campaigns service (WIP)
- [ ] Transactional Campaigns Methods
- [ ] Segments service

### Internal API
- [ ] Websites management
- [ ] Automations
- [ ] Senders API

### Tracking API
- [ ] Tracking API

### SMTP API
- [ ] SMTP
