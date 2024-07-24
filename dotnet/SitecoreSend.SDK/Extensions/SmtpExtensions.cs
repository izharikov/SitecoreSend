using System.Net.Mail;

namespace SitecoreSend.SDK.Extensions;

public static class SmtpExtensions
{
    public static MailMessage AddCampaignId(this MailMessage mailMessage, Guid? campaignId)
    {
        return mailMessage.AddHeader(Constants.SmtpHeaders.CampaignId, campaignId?.ToString());
    }
    
    public static MailMessage AddListId(this MailMessage mailMessage, Guid? listId)
    {
        return mailMessage.AddHeader(Constants.SmtpHeaders.ListId, listId?.ToString());
    }
    
    private static MailMessage AddHeader(this MailMessage mailMessage, string headerName, params string?[] values)
    {
        var value = values.FirstOrDefault(x => !string.IsNullOrEmpty(x));
        if (!string.IsNullOrEmpty(value))
        {
            mailMessage.Headers.Add(headerName, value);
        }

        return mailMessage;
    }
}