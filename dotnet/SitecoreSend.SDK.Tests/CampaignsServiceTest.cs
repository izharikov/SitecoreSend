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
        request.HTMLContent = "<div>Hello, World!</div>";

        var updatedResponse = await _send.Campaigns.UpdateDraft(campaignId, request);
        Assert.NotNull(updatedResponse);
        Assert.True(updatedResponse.Success);

        var removeCampaignResponse = await _send.Campaigns.Delete(campaignId);
        Assert.NotNull(removeCampaignResponse);
        Assert.True(removeCampaignResponse.Success);
    }
}