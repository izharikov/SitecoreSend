using System.Threading.RateLimiting;

namespace SitecoreSend.SDK.Tests.Http;

public class ClientSideRateLimitedHandler(
    RateLimiter limiter)
    : DelegatingHandler(new HttpClientHandler()), IAsyncDisposable
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using var lease = await limiter.AcquireAsync(
            permitCount: 1, cancellationToken);

        if (lease.IsAcquired)
        {
            return await base.SendAsync(request, cancellationToken);
        }

        await Task.Delay(100, cancellationToken);
        return await SendAsync(request, cancellationToken);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    { 
        await limiter.DisposeAsync().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            limiter.Dispose();
        }
    }
}