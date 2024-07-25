namespace SitecoreSend.SDK;

public enum CriteriaType
{
    // member fields:
    DateAdded = 1,
    DateUpdated = 16,
    RecipientName = 2,
    RecipientEmail = 3,
    SubscribeMethod = 14,
    MemberTag = 98,
    RecipientMobile = 30,
    CustomField = 99,
    MailingList = 17,
    MailingListIncluded = 18,
    VerifiedForDoubleOptIn = 25,

    // campaign fields:
    CampaignsOpened = 4,
    LinksClicked = 5,
    CampaignName = 6,
    CampaignTitle = 20,
    LinkURL = 7,
    CampaignSent = 15,
    OpenedAnyCampaign = 24,
    CampaignID = 19,
    SpecificCampaignClicked = 26,
    CampaignIdNotOpened = 27,

    // User agent fields:
    Platform = 8,
    OperatingSystem = 9,
    EmailClient = 10,
    WebBrowser = 11,
    MobileBrowser = 12,

    // Website fields:
    AddedAnythingToCart = 21,
    ViewedProduct = 22,
    PurchasedProduct = 23,
    OrderTotal = 31,
    TotalSpent = 32,
    AverageSpentPerOrder = 33,
    PurchasedProductPrice = 34,
}