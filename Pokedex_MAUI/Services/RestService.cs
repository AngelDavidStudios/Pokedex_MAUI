using System.Text.Json;
using Flurl;
using Flurl.Http;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.Interfaces;

namespace Pokedex_MAUI.Services;

public class RestService : IRestService
{
    public async Task<T> GetResourceAsync<T>(string url)
    {
        var response = await url
            .WithTimeout(TimeSpan.FromSeconds(30))
            .GetJsonAsync<T>();

        return response;
    }

    public async Task<T> GetResourceByNameAsync<T>(string apiEndpoint, string name)
    {
        string sanitizedName = name
            .Replace(" ", "-")
            .Replace("'", "")
            .Replace(".", "");

        var response = await Constants.BASE_URL
            .AppendPathSegments(apiEndpoint, sanitizedName)
            .WithTimeout(TimeSpan.FromSeconds(30))
            .GetJsonAsync<T>();

        return response;
    }
}