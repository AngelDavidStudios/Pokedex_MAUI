using Pokedex_MAUI.Enums;
using Pokedex_MAUI.ObservableCollections;

namespace Pokedex_MAUI.MVVM.Models;

public class WeightFilterModel: ObservableObject
{
    public WeightEnum Weight { get; set; }

    private bool _selected;
    public bool Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }
}