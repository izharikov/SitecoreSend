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
IMailingListService mailingListService = new MailingListService(apiConfiguration);

var response = await mailingListService.GetAllMailingLists();

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
serviceCollection.AddScoped<IMailingListService>(provider => new MailingListService(apiConfiguration,
    provider.GetService<IHttpClientFactory>()!.CreateClient("SitecoreSendClient")));
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
- [ ] Segments service

### Internal API
- [ ] Websites management
- [ ] Automations
- [ ] Senders API

### Tracking API
- [ ] Tracking API

### SMTP API
- [ ] SMTP
