using Newtonsoft.Json;

namespace Pokedex_MAUI.MVVM.Models;

public class NameModel
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("language")]
    public LanguageModel Language { get; set; }
}