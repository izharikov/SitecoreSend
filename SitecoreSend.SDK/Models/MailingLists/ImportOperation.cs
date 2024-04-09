namespace SitecoreSend.SDK
{
    public class ImportOperation
    {
        public string ID { get; set; }
        public string DataHash { get; set; }
        public string Mappings { get; set; }
        public object EmailNotify { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset StartedOn { get; set; }
        public DateTimeOffset CompletedOn { get; set; }
        public int TotalInserted { get; set; }
        public int TotalUpdated { get; set; }
        public int TotalUnsubscribed { get; set; }
        public int TotalInvalid { get; set; }
        public int TotalIgnored { get; set; }
        public int TotalDuplicate { get; set; }
        public int TotalMembers { get; set; }
        public object Message { get; set; }
        public bool Success { get; set; }
        public bool SkipNewMembers { get; set; }
    }
}