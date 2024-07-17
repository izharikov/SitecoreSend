using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK;

public class Sender
{
    public required Guid ID { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CreatedOn { get; set; }
    public bool IsEnabled { get; set; }
    public bool SpfVerified { get; set; }
    public bool DkimVerified { get; set; }
    public string? DkimPublic { get; set; }
}