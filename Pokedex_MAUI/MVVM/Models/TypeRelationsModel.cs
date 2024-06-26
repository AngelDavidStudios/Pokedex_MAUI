using Newtonsoft.Json;

namespace Pokedex_MAUI.MVVM.Models;

public class TypeRelationsModel
{
    [JsonProperty("double_damage_from")]
    public IEnumerable<TypeModel> DoubleDamageFrom { get; set; }

    [JsonProperty("half_damage_from")]
    public IEnumerable<TypeModel> HalfDamageFrom { get; set; }

    [JsonProperty("no_damage_from")]
    public IEnumerable<TypeModel> NoDamageFrom { get; set; }
}