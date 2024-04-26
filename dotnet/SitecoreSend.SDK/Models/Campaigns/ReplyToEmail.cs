namespace SitecoreSend.SDK
{
    public class ReplyToEmail
    {
        public string? ID { get; set; }
        public string? Name { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsEnabled { get; set; }
        public bool SpfVerified { get; set; }
        public bool DkimVerified { get; set; }
        public string? DkimPublic { get; set; }
    }
}