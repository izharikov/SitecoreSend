namespace SitecoreSend.SDK.Transactional;

public class EmailRequestBuilder
{
    private readonly EmailRequest _request;

    private EmailRequestBuilder(Guid campaignId)
    {
        _request = new EmailRequest
        {
            CampaignId = campaignId,
            Personalizations = [],
        };
    }
    
    public static EmailRequestBuilder StartWithCampaign(Guid campaignId)
    {
        return new EmailRequestBuilder(campaignId);
    }

    public EmailRequestBuilder AddPersonalization(Personalization personalization)
    {
        _request.Personalizations.Add(personalization);
        return this;
    }

    public EmailRequestBuilder AddContent(EmailContent content)
    {
        (_request.Content ??= []).Add(content);
        return this;
    }

    public EmailRequestBuilder AddAttachment(EmailAttachment attachment)
    {
        (_request.Attachments ??= []).Add(attachment);
        return this;
    }

    public EmailRequestBuilder Configure(Action<EmailRequest> action)
    {
        action(_request);
        return this;
    }

    public EmailRequest Build()
    {
        return _request;
    }
}