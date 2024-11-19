using System.Text.Json.Serialization;
using SitecoreSend.SDK.JsonConverters;

namespace SitecoreSend.SDK.Internal;

public class Website
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public required string Domain { get; set; }
    public string? Logo { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset CreatedOn { get; set; }
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTimeOffset UpdatedOn { get; set; }
    public bool IsHidden { get; set; }
    public string? Type { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public WebsiteStatus Status { get; set; }
    public string? StatusDescription { get; set; }
}