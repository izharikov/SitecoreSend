namespace SitecoreSend.SDK;

public class SubscribersResponse : BasePagingResponse
{
    public IList<Subscriber> Subscribers { get; set; } = [];
}