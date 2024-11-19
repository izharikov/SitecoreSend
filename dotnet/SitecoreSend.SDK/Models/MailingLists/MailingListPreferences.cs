namespace SitecoreSend.SDK;

public class MailingListPreferences
{
    public string? FallbackValue { get; set; }
    public bool IsRequired { get; set; }
    public IList<string> Options { get; set; } = [];
    public PreferenceSelectType SelectType { get; set; }
}