using Newtonsoft.Json;

namespace Pokedex_MAUI.MVVM.Models;

public class DescriptionModel
{
    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("language")]
    public LanguageModel Language { get; set; }
}