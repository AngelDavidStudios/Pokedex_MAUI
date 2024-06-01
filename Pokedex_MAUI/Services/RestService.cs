using Newtonsoft.Json;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.Interfaces;

namespace Pokedex_MAUI.Services
{
    public class RestService : IRestService
    {
        private readonly HttpClient _httpClient;

        public RestService()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        }

        public async Task<T> GetResourceAsync<T>(string url)
        {
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> GetResourceByNameAsync<T>(string apiEndpoint, string name)
        {
            string sanitizedName = name
                .Replace(" ", "-")
                .Replace("'", "")
                .Replace(".", "");

            var url = $"{Constants.BASE_URL}/{apiEndpoint}/{sanitizedName}";
            var response = await _httpClient.GetStringAsync(url);
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}