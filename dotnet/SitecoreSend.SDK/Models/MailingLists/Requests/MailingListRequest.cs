namespace SitecoreSend.SDK;

public class MailingListRequest
{
    public required string Name { get; set; }
    public string? ConfirmationPage { get; set; }
    public string? RedirectAfterUnsubscribePage { get; set; }
    public MailingListPreferencesRequest Preferences { get; set; } = new();
    public string? PreferencePageId { get; set; }
}