using Pokedex_MAUI.Enums;
using Pokedex_MAUI.ObservableCollections;

namespace Pokedex_MAUI.MVVM.Models;

public class GenerationFilterModel: ObservableObject
{
    public GenerationEnum Generation { get; set; }

    private bool _selected;
    public bool Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }
}