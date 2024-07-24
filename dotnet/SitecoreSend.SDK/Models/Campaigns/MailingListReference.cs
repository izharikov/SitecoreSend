namespace SitecoreSend.SDK;

public class MailingListReference
{
    public string? Campaign { get; set; }
    public Guid? MailingListID { get; set; }
    public MailingList? MailingList { get; set; }
    public int? SegmentID { get; set; }
}