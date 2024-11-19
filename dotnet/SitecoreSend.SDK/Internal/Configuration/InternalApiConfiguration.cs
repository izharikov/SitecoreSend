namespace SitecoreSend.SDK.Internal.Configuration;

public class InternalApiConfiguration : ApiConfiguration
{
    private InternalApiConfiguration()
    {
    }
    
    public static InternalApiConfiguration Create(ApiConfiguration configuration) =>
        new()
        {
            BaseUri = "https://gateway.services.moosend.com",
            ApiKey = configuration.ApiKey,
            Clients = configuration.Clients,
        };
}