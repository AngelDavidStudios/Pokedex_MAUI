using Newtonsoft.Json;

namespace Pokedex_MAUI.MVVM.Models;

public class ChainLinkModel
{
    [JsonProperty("is_baby")]
    public bool IsBaby { get; set; }

    [JsonProperty("species")]
    public PokemonSpeciesModel Species { get; set; }

    [JsonProperty("evolution_details")]
    public IEnumerable<EvolutionDetailsModel> EvolutionDetails { get; set; }

    [JsonProperty("evolves_to")]
    public IEnumerable<ChainLinkModel> EvolvesTo { get; set; }
}