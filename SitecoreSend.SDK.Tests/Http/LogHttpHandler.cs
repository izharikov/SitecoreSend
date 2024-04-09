using Xunit.Abstractions;

namespace SitecoreSend.SDK.Tests.Http
{
    public class LogHttpHandler : DelegatingHandler
    {
        private readonly ITestOutputHelper testOutputHelper;

        public LogHttpHandler(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            testOutputHelper.WriteLine("Request: " + request.ToString());
            if (request.Content != null)
            {
                testOutputHelper.WriteLine(await request.Content.ReadAsStringAsync(cancellationToken));
            }
            testOutputHelper.WriteLine(string.Empty);

            var response = await base.SendAsync(request, cancellationToken);

            testOutputHelper.WriteLine("Response: " + response.ToString());
            testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync(cancellationToken));
            testOutputHelper.WriteLine(string.Empty);

            return response;
        }
    }
}