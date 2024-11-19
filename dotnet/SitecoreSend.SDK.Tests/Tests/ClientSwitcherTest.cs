using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public class ClientSwitcherTest(ITestOutputHelper testOutputHelper)
{

    [Fact]
    public async Task SwitchClients()
    {
        await Task.WhenAll(SwitchToClientAndAssert("Client1"), SwitchToClientAndAssert("Client2"),
            SwitchToClientAndAssert("Client3"), SwitchToClientAndAssert("Client4"));
    }

    private static Random rnd = new Random();

    private async Task SwitchToClientAndAssert(string client)
    {
        using (new ClientSwitcher(client))
        {
            testOutputHelper.WriteLine($"[{client}]: actual value: {ClientSwitcher.Current}");
            Assert.Equal(client, ClientSwitcher.Current);
            await Task.Delay(100 + rnd.Next(10));
            testOutputHelper.WriteLine($"[{client}]: actual value: {ClientSwitcher.Current}");
            Assert.Equal(client, ClientSwitcher.Current);
            await Task.Delay(1000 + rnd.Next(10));
            testOutputHelper.WriteLine($"[{client}]: actual value: {ClientSwitcher.Current}");
            Assert.Equal(client, ClientSwitcher.Current);
            await Task.Delay(200 + rnd.Next(10));
            testOutputHelper.WriteLine($"[{client}]: actual value: {ClientSwitcher.Current}");
            Assert.Equal(client, ClientSwitcher.Current);
        }
        
        Assert.Equal(string.Empty, ClientSwitcher.Current);
    }
}