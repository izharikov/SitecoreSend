using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

[JsonConverter(typeof(CampaignResponseConverter))]
public class CampaignResponse
{
    public Guid ID { get; set; }
    public List<ErrorMessage> Messages { get; set; } = new List<ErrorMessage>();
}

internal class CampaignResponseInternal : CampaignResponse
{
}