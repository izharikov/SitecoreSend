namespace SitecoreSend.SDK;

public class MultipleSubscribersRequest
{
    public bool HasExternalDoubleOptIn { get; set; }
    public IList<SubscriberRequest> Subscribers { get; set; } = [];
}