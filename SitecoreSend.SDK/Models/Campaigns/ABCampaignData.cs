namespace SitecoreSend.SDK
{
    public class ABCampaignData
    {
        public int ID { get; set; }
        public string? SubjectB { get; set; }
        public string? PlainContentB { get; set; }
        public string? HTMLContentB { get; set; }
        public string? WebLocationB { get; set; }
        public Sender? SenderB { get; set; }
        public int HoursToTest { get; set; }
        public int ListPercentage { get; set; }
        public int ABCampaignType { get; set; }
        public int ABWinnerSelectionType { get; set; }
        public DateTime? DeliveredOnA { get; set; }
        public DateTime? DeliveredOnB { get; set; }
    }
}