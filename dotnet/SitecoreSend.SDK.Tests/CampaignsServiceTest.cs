using SitecoreSend.SDK.Tests.Http;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class CampaignsServiceTest(ITestOutputHelper testOutputHelper)
{
#pragma warning disable CA1859
    private readonly ICampaignService _service = new CampaignService(new ApiConfiguration()
#pragma warning restore CA1859
    {
        ApiKey = TestsApp.Configuration.GetSection("SitecoreSend:ApiKey").Value ?? string.Empty,
    }, new HttpClient(new LogHttpHandler(testOutputHelper)));

    [Fact]
    public async Task GetAllCampaigns_OnValidRequest_ShouldReturnAllCampaigns()
    {
        var result = await _service.GetAllCampaigns(1, 100);
        Assert.NotNull(result);
        Assert.True(result.Success);
    }
}