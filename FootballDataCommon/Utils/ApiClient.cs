using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace FootballDataCommon.Utils
{
    public interface IApiClient
    {
        Task<T> get<T>(string url);
    }

    public class ApiClient : IApiClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiClient(IHttpClientFactory clientFactory)
        {
            this._clientFactory = clientFactory;
        }

        public async Task<T> get<T>(string url)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("X-Auth-Token", "cef3de66b69242d88db42d1b0e0a5d8a");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(responseStream);
            }
            else
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<ApiErrorException>(responseStream);
                throw error;
            }
        }

    }
}
