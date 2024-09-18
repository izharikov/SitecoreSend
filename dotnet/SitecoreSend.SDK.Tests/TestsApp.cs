using Microsoft.Extensions.Configuration;
using SitecoreSend.SDK.Tests.Http;
using SitecoreSend.SDK.Tests.Limiter;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public static class TestsApp
{
    public static readonly IConfiguration Configuration = CreateConfiguration();

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
    
    public static ApiConfiguration ApiConfiguration => new()
    {
        ApiKey = Configuration.GetValue("SitecoreSend:ApiKey", string.Empty)!,
        Clients = new Dictionary<string, string>()
        {
            {"Client1", Configuration.GetValue("SitecoreSend:ApiKey2", string.Empty)!},
        },
    };

    private static IConfiguration CreateConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("./appsettings.json");
        builder.AddJsonFile("./appsettings.local.json", optional: true);
        builder.AddEnvironmentVariables();
        var config = builder.Build();
        return config;
    }
}