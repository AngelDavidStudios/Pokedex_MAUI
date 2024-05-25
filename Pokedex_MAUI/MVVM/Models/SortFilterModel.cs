using CommunityToolkit.Mvvm.ComponentModel;
using Pokedex_MAUI.Enums;

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