using Microsoft.Extensions.Configuration;
using SitecoreSend.SDK.Internal;
using SitecoreSend.SDK.Internal.Configuration;
using SitecoreSend.SDK.Internal.Implementations;
using SitecoreSend.SDK.Tests.Http;
using SitecoreSend.SDK.Tests.Limiter;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public static class TestsApp
{
    public static readonly IConfiguration Configuration = CreateConfiguration();

    public static bool SendCampaigns => !Configuration.GetValue("SitecoreSend:SkipSend", false);

    public static readonly Func<ITestOutputHelper, ISendClient> SendFactory = (ITestOutputHelper helper) =>
        new SendClient(ApiConfiguration, () => CustomHttpFactory.Create(helper), new RateLimiterConfiguration()
        {
            Subscribers = new SubscribersWrapper()
            {
                AddSubscriber = SendRateLimits.AddSubscriber.ExecuteAsync,
                AddMultipleSubscribers = SendRateLimits.AddMultipleSubscribers.ExecuteAsync,
                UnsubscribeFromAllLists = SendRateLimits.UnsubscribeFromAllLists.ExecuteAsync,
                UnsubscribeFromList = SendRateLimits.UnsubscribeFromList.ExecuteAsync,
                UnsubscribeFromListAndCampaign = SendRateLimits.UnsubscribeFromListAndCampaign.ExecuteAsync,
            },
        });

    public static readonly Func<ITestOutputHelper, ISendInternalAPI> SendInternalAPIFactory =
        helper => new SendInternalAPIClient(InternalApiConfiguration.Create(ApiConfiguration),
            () => CustomHttpFactory.Create(helper));
    
    public static ApiConfiguration ApiConfiguration => new()
    {
        ApiKey = Configuration.GetValue("SitecoreSend:ApiKey", string.Empty)!,
        Clients = new Dictionary<string, string>()
        {
            {"Client1", Configuration.GetValue("SitecoreSend:ApiKey2", string.Empty)!},
            {"Transactional", Configuration.GetValue("SitecoreSend:TransactionalApiKey", string.Empty)!},
        },
    };

    private static IConfiguration CreateConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("./appsettings.jsonc");
        builder.AddJsonFile("./appsettings.local.jsonc", optional: true);
        builder.AddEnvironmentVariables();
        var config = builder.Build();
        return config;
    }
}