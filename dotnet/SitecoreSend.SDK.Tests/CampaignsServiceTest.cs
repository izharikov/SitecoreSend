using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class CampaignsServiceTest(ITestOutputHelper testOutputHelper)
{

    private readonly ISendClient _send = TestsApp.SendFactory(testOutputHelper);

    [Fact]
    public async Task GetAllCampaigns_OnValidRequest_ShouldReturnAllCampaigns()
    {
        var result = await _send.Campaigns.GetAll(1, 100);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
}