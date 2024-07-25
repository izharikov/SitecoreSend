using System.Net.Http;

namespace SitecoreSend.SDK;

public class SendClient : ISendClient
{
    public ICampaignService Campaigns { get; }
    public IMailingListService Lists { get; }
    public ISubscribersService Subscribers { get; }
    public ISegmentsService Segments { get; }

    public SendClient(ApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory = null,
        RateLimiterConfiguration? rateLimiterConfiguration = null)
    {
        Campaigns = new CampaignService(apiConfiguration, httpClientFactory);
        Lists = new MailingListService(apiConfiguration, httpClientFactory);
        Subscribers = new SubscribersService(apiConfiguration, httpClientFactory,
            rateLimiterConfiguration?.Subscribers);
        Segments = new SegmentsService(apiConfiguration, httpClientFactory);
    }

    public void Dispose()
    {
        Campaigns.Dispose();
        Lists.Dispose();
        Subscribers.Dispose();
    }
}