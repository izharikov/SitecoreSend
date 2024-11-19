using System.Net.Http;
using SitecoreSend.SDK.Internal.Configuration;

namespace SitecoreSend.SDK.Internal.Implementations;

public class WebsitesClient : BaseInternalSendClient, IWebsitesClient
{
    public WebsitesClient(InternalApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory) : base(apiConfiguration, httpClientFactory)
    {
    }

    public Task<IList<Website>?> GetAll(CancellationToken? cancellationToken = null)
    {
        return Request<IList<Website>>(HttpMethod.Get, "/websites", null, cancellationToken);
    }
}