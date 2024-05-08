using Microsoft.Extensions.Configuration;
using SitecoreSend.SDK.Tests.Http;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests;

public static class TestsApp
{
    public static readonly IConfiguration Configuration = CreateConfiguration();

    public static ApiConfiguration ApiConfiguration => new ApiConfiguration()
    {
        ApiKey = TestsApp.Configuration.GetValue("SitecoreSend:ApiKey", string.Empty)!,
    };

    public static Func<ITestOutputHelper, HttpClient> Client = (helper) =>
        new HttpClient(new LogHttpHandler(helper,
            Configuration.GetValue<bool>("UnitTests:HideSecrets")));
    
    private static IConfiguration CreateConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("./appsettings.json");
        builder.AddJsonFile("./appsettings.local.json", optional:true);
        builder.AddEnvironmentVariables();
        var config = builder.Build();
        return config;
    }
}