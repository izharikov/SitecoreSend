using Microsoft.Extensions.Configuration;

namespace SitecoreSend.SDK.Tests;

public static class TestsApp
{
    public static readonly IConfiguration Configuration = CreateConfiguration();

    public static ApiConfiguration ApiConfiguration => new()
    {
        ApiKey = Configuration.GetValue("SitecoreSend:ApiKey", string.Empty)!,
    };

    private static IConfiguration CreateConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("./appsettings.json");
        builder.AddJsonFile("./appsettings.local.json", optional: true);
        builder.AddEnvironmentVariables();
        var config = builder.Build();
        return config;
    }
}