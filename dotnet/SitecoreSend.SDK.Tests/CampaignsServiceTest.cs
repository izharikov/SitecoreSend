using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class CampaignsServiceTest(ITestOutputHelper testOutputHelper)
{
    private readonly ISendClient _send = TestsApp.SendFactory(testOutputHelper);

    [Fact]
    public async Task GetAll_OnValidRequest_ShouldReturnAllCampaigns()
    {
        var result = await _send.Campaigns.GetAll(1, 100);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task Get_OnKnownCampaign_ShouldReturnCampaign()
    {
        var campaign = TestsApp.Configuration.GetSection("SitecoreSend:CampaignId").Value!;
        var campaignId = Guid.Parse(campaign);
        var result = await _send.Campaigns.Get(campaignId);
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.True(result.Data?.ID == campaignId);
    }

    [Fact]
    public async Task GetAllSenders_OnValidRequest_ShouldReturnKnownSender()
    {
        var knownSender = TestsApp.Configuration.GetSection("SitecoreSend:KnownSender").Value!;
        var senders = await _send.Campaigns.GetAllSenders();
        Assert.True(senders?.Success);
        Assert.True(senders?.Data?.Select(x => x.Email).Contains(knownSender));
    }

    [Fact]
    public async Task GetSender_OnKnownSender_ShouldReturnDetails()
    {
        var knownSender = TestsApp.Configuration.GetSection("SitecoreSend:KnownSender").Value!;
        var sender = await _send.Campaigns.GetSender(knownSender);
        Assert.True(sender?.Success);
        Assert.NotNull(sender);
        Assert.NotNull(sender.Data);
        Assert.Equal(sender.Data.Email, knownSender);
    }

    [Fact]
    public async Task Campaigns_OnCreateUpdateDelete_ShouldPerformOperations()
    {
        var knownSender = TestsApp.Configuration.GetSection("SitecoreSend:KnownSender").Value!;
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        // create campaign, but with empty MailingLists and no email content
        var request = new CampaignRequest()
        {
            Name = "API Campaign",
            Subject = "API Subject",
            SenderEmail = knownSender,
        };
        var newCampaign = await _send.Campaigns.CreateDraft(request);
        Assert.NotNull(newCampaign);
        Assert.False(newCampaign.Success);
        Assert.True(newCampaign.Data?.ID != Guid.Empty);
        Assert.NotEmpty(newCampaign.Data?.Messages ?? []);

        var campaignId = newCampaign.Data!.ID;

        var campaign = await _send.Campaigns.Get(campaignId);
        Assert.NotNull(campaign);
        Assert.True(campaign.Success);

        request.Name = "API Campaign Updated";
        request.MailingLists = [new MailingListReference() {MailingListID = listId}];
        request.HTMLContent = "<div>Hi, #recipient:name#! Hello, World!</div>";

        var updatedResponse = await _send.Campaigns.UpdateDraft(campaignId, request);
        Assert.NotNull(updatedResponse);
        Assert.True(updatedResponse.Success);

        campaign = await _send.Campaigns.Get(campaignId);
        Assert.NotNull(campaign);
        Assert.True(campaign.Success);
        Assert.Equal("API Campaign Updated", campaign.Data?.Name);
        Assert.Equal(listId, campaign.Data?.MailingLists.FirstOrDefault()?.MailingListID);
        Assert.Equal(0, campaign.Data?.MailingLists.FirstOrDefault()?.SegmentID);


        var removeCampaignResponse = await _send.Campaigns.Delete(campaignId);
        Assert.NotNull(removeCampaignResponse);
        Assert.True(removeCampaignResponse.Success);
    }

    [Fact]
    public async Task Campaigns_CreateCampaignSend_ShouldPerformOperations()
    {
        var knownSender = TestsApp.Configuration.GetSection("SitecoreSend:KnownSender").Value!;
        var listId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:TestListId").Value!);
        var testEmail = TestsApp.Configuration.GetSection("SitecoreSend:TestEmail").Value!;
        var request = new CampaignRequest()
        {
            Name = $"API Campaign {DateTimeOffset.Now:dd-MM-yyyy HH:mm} {Guid.NewGuid()}",
            Subject = "API Subject",
            SenderEmail = knownSender,
            MailingLists = [new MailingListReference() {MailingListID = listId}],
            HTMLContent = "<div>Hi, #recipient:name|User#! Hello, World!</div>",
        };
        var newCampaign = await _send.Campaigns.CreateDraft(request);

        Assert.NotNull(newCampaign);
        Assert.True(newCampaign.Success);

        var sendTest = await _send.Campaigns.SendTest(newCampaign.Data!.ID, [testEmail]);

        Assert.True(sendTest?.Success);

        var sendCampaign = await _send.Campaigns.Send(newCampaign.Data!.ID);
        Assert.True(sendCampaign?.Success);
    }

    [Fact]
    public async Task Campaigns_GetStatistics_ShouldReturnCorrectStats()
    {
        var abCampaignId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:AbCampaignId").Value!);
        var abSummary = await _send.Campaigns.GetABCampaignSummary(abCampaignId);

        Assert.True(abSummary?.Success);
        Assert.NotNull(abSummary?.Data?.A);
        Assert.NotNull(abSummary.Data?.B);
        Assert.NotEmpty(abSummary.Data.A.MailingLists);
        Assert.NotEmpty(abSummary.Data.A.MailingLists.Where(x => x.MailingList != null || x.MailingListID != null));
        Assert.NotEmpty(abSummary.Data.B.MailingLists);
        Assert.NotEmpty(abSummary.Data.B.MailingLists.Where(x => x.MailingList != null || x.MailingListID != null));

        var campaignId = Guid.Parse(TestsApp.Configuration.GetSection("SitecoreSend:CampaignId").Value!);

        var summary = await _send.Campaigns.GetSummary(campaignId);

        Assert.True(summary?.Success);
        Assert.NotNull(summary?.Data);
        Assert.NotEmpty(summary.Data.MailingLists);
        Assert.NotEmpty(summary.Data.MailingLists.Where(x => x.MailingList != null || x.MailingListID != null));

        var sentStats = await _send.Campaigns.GetStatistics(campaignId, ActivityType.Sent);
        Assert.True(sentStats?.Success);

        var openStats = await _send.Campaigns.GetStatistics(campaignId, ActivityType.Opened);
        Assert.True(openStats?.Success);
        
        var locationStats = await _send.Campaigns.GetActivityByLocation(campaignId);
        Assert.True(locationStats?.Success);
        
        var linkActivity = await _send.Campaigns.GetLinkActivity(campaignId);
        Assert.True(linkActivity?.Success);

        var fullStats =
            await _send.Campaigns.GetStatistics(campaignId, ActivityType.Sent, 1, 100,
                DateTimeOffset.Now.AddMonths(-5));
        Assert.True(fullStats?.Success);
    }
}