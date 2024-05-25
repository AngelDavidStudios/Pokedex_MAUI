using Newtonsoft.Json;

namespace Pokedex_MAUI.MVVM.Models;

public class PokemonSpeciesDexEntryModel
{
    [JsonProperty("entry_number")]
    public int EntryNumber { get; set; }

    [JsonProperty("pokedex")]
    public PokedexModel Pokedex { get; set; }
}