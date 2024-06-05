using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace SitecoreSend.SDK;

public abstract class BaseApiService
{
    private const string Format = ".json";

    private HttpClient _httpClient => EnsureClient(_factory() ?? _defaultClient ?? CreateDefaultClient(_apiConfiguration));
    private readonly HttpClient? _defaultClient;
    private readonly Func<HttpClient?> _factory;
    private readonly ApiConfiguration _apiConfiguration;

    protected BaseApiService(ApiConfiguration apiConfiguration, Func<HttpClient?> httpClientFactory)
    {
        _apiConfiguration = apiConfiguration;
        _factory = httpClientFactory;
        _defaultClient = null;
    }

    protected BaseApiService(ApiConfiguration apiConfiguration) : this(apiConfiguration, () => null)
    {
        _defaultClient = CreateDefaultClient(apiConfiguration);
    }

    protected static HttpClient CreateDefaultClient(ApiConfiguration apiConfiguration)
    {
        return new HttpClient()
        {
            BaseAddress = new Uri(apiConfiguration.BaseUri),
        };
    }

    protected string Url(string baseUrl, params object?[] queryParams)
    {
        var result = new StringBuilder(baseUrl);
        result.Append(Format);
        result.Append("?apiKey=").Append(_apiConfiguration.ApiKey);
        for (var i = 0; i < queryParams.Length; i += 2)
        {
            if (!string.IsNullOrEmpty(queryParams[i]?.ToString()) &&
                !string.IsNullOrEmpty(queryParams[i + 1]?.ToString()))
            {
                result.Append('&').Append(queryParams[i]).Append('=')
                    .Append(queryParams[i + 1]);
            }
        }

        return result.ToString();
    }

    protected async Task<T?> Get<T>(string url, CancellationToken? cancellationToken = null) where T : SendResponse
    {
        return await Get<T>(_httpClient, url, cancellationToken);
    }

    protected async Task<T?> Get<T>(HttpClient client, string url, CancellationToken? cancellationToken = null) where T : SendResponse
    {
        var response = await client.GetAsync(url, cancellationToken ?? CancellationToken.None);
        return await ReadResponse<T>(response).ConfigureAwait(false);
    }

    protected async Task<T?> Delete<T>(string url, CancellationToken? cancellationToken = null) where T : SendResponse
    {
        return await Delete<T>(_httpClient, url, cancellationToken);
    }

    protected async Task<T?> Delete<T>(HttpClient client, string url,
        CancellationToken? cancellationToken = null) where T : SendResponse
    {
        var response = await client.DeleteAsync(url, cancellationToken ?? CancellationToken.None)
            .ConfigureAwait(false);
        return await ReadResponse<T>(response).ConfigureAwait(false);
    }

    protected async Task<TResponse?> Post<TResponse>(string url, object body,
        CancellationToken? cancellationToken = null) where TResponse : SendResponse
    {
        return await Post<TResponse>(_httpClient, url, body, cancellationToken);
    }

    protected async Task<TResponse?> Post<TResponse>(HttpClient client, string url, object body,
        CancellationToken? cancellationToken = null) where TResponse : SendResponse
    {
        var response = await client.PostAsJsonAsync(url, body, cancellationToken ?? CancellationToken.None)
            .ConfigureAwait(false);
        return await ReadResponse<TResponse>(response).ConfigureAwait(false);
    }

    protected async Task<TResponse?> ReadResponse<TResponse>(HttpResponseMessage response) where TResponse : SendResponse
    {
        var result = await response.Content.ReadFromJsonAsync<TResponse>();
        if (result?.Error == KnownErrors.RATE_LIMITING)
        {
            result.RateLimitDetails = new RateLimitDetails()
            {
                FirstCall = GetDateHeader(response, "x-ratelimit-firstcall"),
                Expires = GetDateHeader(response, "x-ratelimit-expires"),
            };
        }

        return result;
    }

    private HttpClient EnsureClient(HttpClient client)
    {
        if (client.BaseAddress == null)
        {
            client.BaseAddress = new Uri(_apiConfiguration.BaseUri);
        }

        return client;
    }

    private static DateTimeOffset? GetDateHeader(HttpResponseMessage response, string key)
    {
        if (response.Headers.TryGetValues(key, out var values))
        {
            if (DateTimeOffset.TryParseExact(values.FirstOrDefault(), "MM/dd/yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var result))
            {
                return result;
            }
        }

        return null;
    }
}