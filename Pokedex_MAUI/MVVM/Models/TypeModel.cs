using Newtonsoft.Json;
using Pokedex_MAUI.Enums;
using Pokedex_MAUI.Extensions;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.MVVM.Models.Base;

namespace Pokedex_MAUI.MVVM.Models;

public class TypeModel: ResourceBaseModel
{
    [JsonProperty("id")]
    public override int Id { get; set; }

    [JsonProperty("name")]
    public override string Name { get; set; }

    public override string NameFirstCharUpper => Name.FirstCharToUpper();

    public override string NameUpperCase => Name.ToUpper();

    public override string ApiEndpoint => Constants.ENDPOINT_TYPE;

    [JsonProperty("damage_relations")]
    public TypeRelationsModel DamageRelations { get; set; }

    public IEnumerable<TypeRelationModel> AllTypeRelations { get; set; }

    public TypeEnum Type
    {
        get
        {
            if (Enum.TryParse(NameFirstCharUpper, out TypeEnum type))
                return type;
            else
                return TypeEnum.Undefined;
        }
    }
}