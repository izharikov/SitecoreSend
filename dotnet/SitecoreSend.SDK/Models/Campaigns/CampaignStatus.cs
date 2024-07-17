namespace SitecoreSend.SDK;

public enum CampaignStatus
{
    Draft = 0,
    QueuedForSending = 1,
    Sent = 3,
    NotEnoughCredits = 4,
    AwaitingDelivery = 5,
    Sending = 6,
    Deleted = 10,
    SelectingWinner = 11,
    Archived = 12,
    SubscriptionExpired = 13,
    SubscriptionLimitsExceeded = 14,
}