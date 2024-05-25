using Newtonsoft.Json;
using Pokedex_MAUI.Extensions;
using Pokedex_MAUI.Helpers;

namespace PokedexXF.Models
{
    public class GenerationModel : ResourceBaseModel
    {
        [JsonProperty("id")]
        public override int Id { get; set; }

        [JsonProperty("name")]
        public override string Name { get; set; }

        public override string NameFirstCharUpper => Name.FirstCharToUpper();

        public override string NameUpperCase => Name.ToUpper();

        public override string ApiEndpoint => Constants.ENDPOINT_GENERATION;

        [JsonProperty("pokemon_species")]
        public IEnumerable<PokemonSpeciesModel> Species { get; set; }
    }
}
