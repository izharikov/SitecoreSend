namespace SitecoreSend.SDK;

public class ApiConfiguration
{
    public string BaseUri { get; set; } = "https://api.sitecoresend.io/v3/";
    public required string ApiKey { get; set; }
}