using System.Text.Json.Serialization;

namespace SitecoreSend.SDK.Transactional;

public class EmailAttachment
{
    public required string Content { get; set; }
    public required string Type { get; set; }
    public required string FileName { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AttachmentDisposition Disposition { get; set; } = AttachmentDisposition.Attachment;

    public string? ContentId { get; set; }
}