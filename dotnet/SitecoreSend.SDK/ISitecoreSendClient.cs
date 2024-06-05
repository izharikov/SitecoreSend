namespace SitecoreSend.SDK;

// TODO: idea to use it like _sitecoreSend.Lists.GetAll(...
internal interface ISitecoreSendClient
{
    ICampaignService Campaigns { get; }
    IMailingListService Lists { get; }
    ISubscribersService Subscribers { get; }
}