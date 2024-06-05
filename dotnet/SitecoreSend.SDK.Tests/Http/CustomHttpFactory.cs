using System.Threading.RateLimiting;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests.Http;

public static class CustomHttpFactory
{
    public static readonly Func<ITestOutputHelper, HttpClient> Create = (helper) =>
        new HttpClient(new LogHttpHandler(helper, new HttpClientHandler(), TestsApp.Configuration.GetValue<bool>("UnitTests:HideSecrets")));

    public delegate HttpClient CreateRateClient(ITestOutputHelper helper, int windowsInSeconds, int permitLimit);
}