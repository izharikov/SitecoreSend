using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests.Http;

public partial class LogHttpHandler : DelegatingHandler
{
    private readonly ITestOutputHelper testOutputHelper;
    private readonly bool _hideSecrets;

    public LogHttpHandler(ITestOutputHelper testOutputHelper, HttpMessageHandler handler, bool hideSecrets = true)
    {
        this.testOutputHelper = testOutputHelper;
        _hideSecrets = hideSecrets;
        InnerHandler = handler;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        testOutputHelper.WriteLine("Request: " + HideSecrets(request.ToString()));
        if (request.Content != null)
        {
            testOutputHelper.WriteLine(HideSecrets(await request.Content.ReadAsStringAsync(cancellationToken)));
        }

        testOutputHelper.WriteLine(string.Empty);

        var response = await base.SendAsync(request, cancellationToken);

        testOutputHelper.WriteLine("Response: " + HideSecrets(response.ToString()));
        testOutputHelper.WriteLine(HideSecrets(await response.Content.ReadAsStringAsync(cancellationToken)));
        testOutputHelper.WriteLine(string.Empty);

        return response;
    }

    private static readonly Regex GuidRegex = GeneratedGuidRegex();
    private static readonly Regex EmailRegex = GeneratedEmailRegex();

    private string HideSecrets(string input)
    {
        if (!_hideSecrets)
        {
            return input;
        }
        var hidden = GuidRegex.Replace(input, "********-****-****-****-************");
        hidden = EmailRegex.Replace(hidden, match => match.Groups[1] + "*****@***.***");
        return hidden;
    }

    [GeneratedRegex(@"[a-fA-F\d]{8}-[a-fA-F\d]{4}-[a-fA-F\d]{4}-[a-fA-F\d]{4}-[a-fA-F\d]{12}")]
    private static partial Regex GeneratedGuidRegex();

    [GeneratedRegex(
        @"([\=""]{1})(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)")]
    private static partial Regex GeneratedEmailRegex();
}