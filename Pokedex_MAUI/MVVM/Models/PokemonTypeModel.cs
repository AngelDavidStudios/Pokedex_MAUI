using Newtonsoft.Json;

namespace Pokedex_MAUI.MVVM.Models;

public class PokemonTypeModel
{
    [JsonProperty("slot")]
    public int Slot { get; set; }

    [JsonProperty("type")]
    public TypeModel Type { get; set; }

    public bool IsBusy { get; set; }
}