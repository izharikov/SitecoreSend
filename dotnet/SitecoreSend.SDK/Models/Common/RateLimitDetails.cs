namespace SitecoreSend.SDK;

public class RateLimitDetails
{
    public DateTimeOffset? FirstCall { get; set; }
    public DateTimeOffset? Expires { get; set; }
}