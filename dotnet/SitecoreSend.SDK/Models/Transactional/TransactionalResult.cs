namespace SitecoreSend.SDK.Transactional;

public class TransactionalResult
{
    public IList<ExcludedRecipient> ExcludedRecipients { get; set; } = new List<ExcludedRecipient>();
    public int TotalAccepted { get; set; }
    public int TotalExcluded { get; set; }
}