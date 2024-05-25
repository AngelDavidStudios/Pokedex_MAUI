using System.Text.Json;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.Interfaces;

namespace Pokedex_MAUI.Services;

public class LiteDbService: IRestService
{
    private readonly HttpClient _httpClient;

    public LiteDbService()
    {
        _httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
    }
    
    public async Task<T> GetResourceAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content);
        }

        return default;
    }
    
    public async Task<T> GetResourceByNameAsync<T>(string apiEndpoint, string name)
    {
        string sanitizedName = name
            .Replace(" ", "-")
            .Replace("'", "")
            .Replace(".", "");

        var url = $"{Constants.BASE_URL}/{apiEndpoint}/{sanitizedName}";
        return await GetResourceAsync<T>(url);
    }
}