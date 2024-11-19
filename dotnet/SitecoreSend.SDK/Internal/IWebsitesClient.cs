namespace SitecoreSend.SDK.Internal;

public interface IWebsitesClient
{
    Task<IList<Website>?> GetAll(CancellationToken? cancellationToken = null);
}