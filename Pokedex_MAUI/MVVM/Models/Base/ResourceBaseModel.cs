using CommunityToolkit.Mvvm.ComponentModel;

namespace Pokedex_MAUI.MVVM.Models.Base;

public abstract class ResourceBaseModel: ObservableObject
{
    public abstract int Id { get; set; }
    public abstract string Name { get; set; }
    public abstract string NameFirstCharUpper { get; }
    public abstract string NameUpperCase { get; }
    public abstract string ApiEndpoint { get; }
}