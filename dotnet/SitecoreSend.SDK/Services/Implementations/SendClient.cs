using System.Net.Http;

namespace SitecoreSend.SDK;

public class SendClient : ISendClient
{
    public ICampaignService Campaigns { get; }
    public IMailingListService Lists { get; }
    public ISubscribersService Subscribers { get; }

    public SendClient(ApiConfiguration apiConfiguration, Func<HttpClient>? httpClientFactory = null,
        RateLimiterConfiguration? rateLimiterConfiguration = null, bool disposeHttpClient = false)
    {
        var client = httpClientFactory?.Invoke() ?? BaseApiService.CreateDefaultClient(apiConfiguration);
        // use single client for all services
        var factory = () => client;
        Campaigns = new CampaignService(apiConfiguration, factory, disposeHttpClient);
        Lists = new MailingListService(apiConfiguration, factory, disposeHttpClient);
        Subscribers = new SubscribersService(apiConfiguration, factory,
            rateLimiterConfiguration?.Subscribers, disposeHttpClient);
    }

    public void Dispose()
    {
        Campaigns.Dispose();
        Lists.Dispose();
        Subscribers.Dispose();
    }
}