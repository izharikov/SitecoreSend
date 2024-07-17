namespace SitecoreSend.SDK;

public enum ActivityType
{
    Sent, // when and to which recipients the campaign was sent
    Opened, // who opened the campaign
    LinkClicked, // who clicked on which links in the campaign
    Forward, // who forwarded the campaign using the relevant link in the email body and when
    Unsubscribed, // who unsubscribed from the campaign by clicking the unsubscribe link and when
    Bounced, // which email recipients failed to receive the campaign
    Complained, // which email recipients reported your campaign as spam through their email service
    Activity, // all types of activities for the campaign
}