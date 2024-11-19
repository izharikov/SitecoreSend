using System.Net.Http;
using SitecoreSend.SDK.Internal.Configuration;

namespace SitecoreSend.SDK.Internal.Implementations;

public class SendInternalAPIClient : ISendInternalAPI
{
    public IWebsitesClient Websites { get; }

    public SendInternalAPIClient(InternalApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory = null)
    {
        Websites = new WebsitesClient(apiConfiguration, httpClientFactory);
    }
}