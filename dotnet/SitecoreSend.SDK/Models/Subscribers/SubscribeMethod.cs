namespace SitecoreSend.SDK;

public enum SubscribeMethod
{
    Unknown = 0,
    Ui = 1,
    Api = 2,
    Form = 3,
    Import = 4,
    Automation = 5,
    ZapierPlugin = 6,
    MailchimpPlugin = 7,
    WebsiteIdentified = 8,
    SmtpDispatching = 9,
    SitecoreConnectPlugin = 10,
    OtherPlugin = 100,
}