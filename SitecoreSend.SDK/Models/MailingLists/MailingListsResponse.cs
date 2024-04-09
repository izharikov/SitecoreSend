namespace SitecoreSend.SDK
{
    public class MailingListsResponse : BasePagingResponse
    {
        public IList<MailingList> MailingLists { get; set; } = [];
    }
}