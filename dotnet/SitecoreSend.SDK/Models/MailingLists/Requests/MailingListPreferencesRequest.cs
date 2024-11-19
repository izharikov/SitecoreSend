using System.Text.Json.Serialization;

namespace SitecoreSend.SDK;

public class MailingListPreferencesRequest
{
    public string? FallbackValue { get; set; }
    public bool IsRequired { get; set; }
    public IList<string> Options { get; set; } = [];
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PreferenceSelectType SelectType { get; set; }
}