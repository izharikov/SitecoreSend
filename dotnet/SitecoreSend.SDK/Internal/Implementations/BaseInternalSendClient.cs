using System.Net.Http;
using System.Net.Http.Json;
using SitecoreSend.SDK.Internal.Configuration;

namespace SitecoreSend.SDK.Internal.Implementations;

public abstract class BaseInternalSendClient: IDisposable
{
    private HttpClient _httpClient =>
        EnsureClient(_factory?.Invoke() ?? (_internalClient ??= CreateDefaultClient(_apiConfiguration)));

    // if _factory is null or _factory result is null then initialize default client
    private HttpClient? _internalClient;
    private readonly Func<HttpClient?>? _factory;
    private readonly InternalApiConfiguration _apiConfiguration;

    protected BaseInternalSendClient(InternalApiConfiguration apiConfiguration, Func<HttpClient?>? httpClientFactory)
    {
        _apiConfiguration = apiConfiguration;
        _factory = httpClientFactory;
    }

    private static HttpClient CreateDefaultClient(InternalApiConfiguration apiConfiguration)
    {
        return new HttpClient()
        {
            BaseAddress = new Uri(apiConfiguration.BaseUri),
        };
    }

    private string GetCurrentApiKey()
    {
        var key = _apiConfiguration.ApiKey;
        var client = ClientSwitcher.Current;
        if (!string.IsNullOrEmpty(client) && _apiConfiguration.Clients.TryGetValue(client, out var val) &&
            !string.IsNullOrEmpty(val))
        {
            key = val;
        }

        return key;
    }
    
    private HttpClient EnsureClient(HttpClient client)
    {
        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(_apiConfiguration.BaseUri);
        }

        return client;
    }

    protected async Task<T?> Request<T>(HttpMethod method, string url, object? body,
        CancellationToken? cancellationToken)
    {
        var response = await _httpClient.SendAsync(new HttpRequestMessage()
            {
                Method = method,
                Headers =
                {
                    { "X-Apikey", GetCurrentApiKey() },
                    { "Accept", "application/json" },
                },
                RequestUri = new Uri(url, UriKind.Relative),
                Content = body != null ? JsonContent.Create(body) : null,
            }, cancellationToken ?? CancellationToken.None)
            .ConfigureAwait(false);
        return await response.Content.ReadFromJsonAsync<T?>().ConfigureAwait(false);
    }
    
    public void Dispose()
    {
        _internalClient?.Dispose();
    }
}