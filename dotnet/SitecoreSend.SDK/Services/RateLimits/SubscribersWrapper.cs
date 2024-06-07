namespace SitecoreSend.SDK;

public class SubscribersWrapper
{
    public Wrapper<SendResponse<Subscriber>>? AddSubscriber { get; set; }
    public Wrapper<SendResponse<IList<Subscriber>>>? AddMultipleSubscribers { get; set; }
    public Wrapper<SendResponse>? UnsubscribeFromAllLists { get; set; }
    public Wrapper<SendResponse>? UnsubscribeFromList { get; set; }
    public Wrapper<SendResponse>? UnsubscribeFromListAndCampaign { get; set; }
}