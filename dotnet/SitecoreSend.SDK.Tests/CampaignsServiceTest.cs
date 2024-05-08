using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class CampaignsServiceTest(ITestOutputHelper testOutputHelper)
{

    private readonly ICampaignService _service = new CampaignService(TestsApp.ApiConfiguration, TestsApp.Client(testOutputHelper));

    [Fact]
    public async Task GetAllCampaigns_OnValidRequest_ShouldReturnAllCampaigns()
    {
        var result = await _service.GetAllCampaigns(1, 100);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
}