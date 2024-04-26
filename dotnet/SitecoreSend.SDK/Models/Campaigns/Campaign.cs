namespace SitecoreSend.SDK
{
    using System;
    using System.Collections.Generic;

    public class Campaign
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? WebLocation { get; set; }
        public string? HTMLContent { get; set; }
        public string? PlainContent { get; set; }
        public List<Sender> Sender { get; set; } = [];
        public ReplyToEmail? ReplyToEmail { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime? ScheduledFor { get; set; }
        public string? TimeZone { get; set; }
        public int FormatType { get; set; }
        public ABCampaignData? ABCampaignData { get; set; }
        public List<MailingListReference> MailingLists { get; set; } = [];
        public string? ConfirmationTo { get; set; }
        public string? Status { get; set; }
        public bool IsTransactional { get; set; }
    }
}