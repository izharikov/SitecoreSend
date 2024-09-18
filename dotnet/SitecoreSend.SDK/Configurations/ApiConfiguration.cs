namespace SitecoreSend.SDK;

public class ApiConfiguration
{
    public string BaseUri { get; set; } = "https://api.sitecoresend.io/v3/";
    public required string ApiKey { get; set; }
    public IDictionary<string, string> Clients { get; set; } = new Dictionary<string, string>();
}