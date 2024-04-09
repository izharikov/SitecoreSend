using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace SitecoreSend.SDK
{
    public abstract class BaseApiService
    {
        private const string Format = ".json";

        private readonly HttpClient _httpClient;
        private readonly ApiConfiguration _apiConfiguration;

        // TODO: handle http errors

        protected BaseApiService(ApiConfiguration apiConfiguration, HttpClient httpClient)
        {
            _apiConfiguration = apiConfiguration;
            _httpClient = httpClient;
            httpClient.BaseAddress = new Uri(apiConfiguration.BaseUri);
        }
        
        protected BaseApiService(ApiConfiguration apiConfiguration) : this(apiConfiguration, new HttpClient()
        {
            BaseAddress = new Uri(apiConfiguration.BaseUri),
        })
        {
        }

        protected string Url(string baseUrl, params object?[] queryParams)
        {
            var result = new StringBuilder(baseUrl);
            result.Append(Format);
            result.Append("?apiKey=").Append(_apiConfiguration.ApiKey);
            for (var i = 0; i < queryParams.Length; i += 2)
            {
                if (!string.IsNullOrEmpty(queryParams[i]?.ToString()) && !string.IsNullOrEmpty(queryParams[i + 1]?.ToString()))
                {
                    result.Append('&').Append(queryParams[i]).Append('=')
                        .Append(queryParams[i + 1]);
                }
            }

            return result.ToString();
        }

        protected async Task<T?> Get<T>(string url, CancellationToken? cancellationToken = null)
        {
            return await _httpClient.GetFromJsonAsync<T>(url, cancellationToken ?? CancellationToken.None)
                .ConfigureAwait(false);
        }

        protected async Task<T?> Delete<T>(string url, CancellationToken? cancellationToken = null)
        {
            var response = await _httpClient.DeleteAsync(url, cancellationToken ?? CancellationToken.None)
                .ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        }

        protected async Task<TResponse?> Post<TResponse>(string url, object body,
            CancellationToken? cancellationToken = null)
        {
            var response = await _httpClient.PostAsJsonAsync(url, body, cancellationToken ?? CancellationToken.None)
                .ConfigureAwait(false);
            return await response.Content.ReadFromJsonAsync<TResponse>().ConfigureAwait(false);
        }
    }
}