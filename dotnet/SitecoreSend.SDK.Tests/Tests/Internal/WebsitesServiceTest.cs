using SitecoreSend.SDK.Internal;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests.Internal;

public class WebsitesServiceTest(ITestOutputHelper testOutputHelper)
{
    private readonly ISendInternalAPI _internalApi = TestsApp.SendInternalAPIFactory(testOutputHelper);

    [Fact]
    public async Task GetAll_OnValidRequest_ShouldReturnAllWebsites()
    {
        var result = await _internalApi.Websites.GetAll();
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
    }
}