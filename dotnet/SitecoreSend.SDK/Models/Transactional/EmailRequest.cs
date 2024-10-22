namespace SitecoreSend.SDK.Transactional;

public class EmailRequest
{
    public EmailSender? From { get; set; }
    public EmailSender? ReplyTo { get; set; }
    public string? Subject { get; set; }
    public required Guid CampaignId { get; set; }
    public Guid? TemplateId { get; set; }
    public string? TemplateName { get; set; }
    public List<EmailContent>? Content { get; set; }
    public required List<Personalization> Personalizations { get; set; }
    public MailSettings? MailSettings { get; set; }
    public List<EmailAttachment>? Attachments { get; set; }
}