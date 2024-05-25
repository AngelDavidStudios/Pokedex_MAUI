using Newtonsoft.Json;
using Pokedex_MAUI.Helpers;

namespace Pokedex_MAUI.MVVM.Models;

public class ResourceItemModel
{
    public int Id { get => PokemonHelper.ExtractIdFromUrl(Url); }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }
}