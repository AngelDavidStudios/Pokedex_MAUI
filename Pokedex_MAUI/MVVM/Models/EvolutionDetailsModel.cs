using Newtonsoft.Json;

namespace Pokedex_MAUI.MVVM.Models;

public class EvolutionDetailsModel
{
    [JsonProperty("min_level")]
    public int? MinLevel { get; set; }
}