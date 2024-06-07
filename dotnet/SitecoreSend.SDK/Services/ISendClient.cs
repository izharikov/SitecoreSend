namespace SitecoreSend.SDK;

public interface ISendClient : IDisposable
{
    ICampaignService Campaigns { get; }
    IMailingListService Lists { get; }
    ISubscribersService Subscribers { get; }
}