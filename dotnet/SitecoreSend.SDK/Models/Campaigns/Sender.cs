namespace SitecoreSend.SDK
{
    public class Sender
    {
        public string? ID { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsEnabled { get; set; }
        public bool SpfVerified { get; set; }
        public bool DkimVerified { get; set; }
        public string? DkimPublic { get; set; }
        public DateTime? DeliveredOn { get; set; }
    }
}