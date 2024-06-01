using Pokedex_MAUI.Enums;
using Pokedex_MAUI.ObservableCollections;

namespace Pokedex_MAUI.MVVM.Models;

public class SortFilterModel: ObservableObject
{
    public SortEnum Sort { get; set; }

    private bool _selected;
    public bool Selected
    {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }
}