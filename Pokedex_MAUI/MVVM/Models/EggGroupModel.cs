using Newtonsoft.Json;
using Pokedex_MAUI.Extensions;
using Pokedex_MAUI.Helpers;
using Pokedex_MAUI.MVVM.Models.Base;

namespace Pokedex_MAUI.MVVM.Models;

public class EggGroupModel: ResourceBaseModel
{
    public override int Id { get; set; }

    [JsonProperty("name")]
    public override string Name { get; set; }

    public override string NameFirstCharUpper => Name.FirstCharToUpper();

    public override string NameUpperCase => Name.ToUpper();

    public override string ApiEndpoint => Constants.ENDPOINT_EGG_GROUP;
}