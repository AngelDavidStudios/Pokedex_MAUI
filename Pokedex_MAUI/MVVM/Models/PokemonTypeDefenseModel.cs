using CommunityToolkit.Mvvm.ComponentModel;
using Pokedex_MAUI.Enums;

namespace Pokedex_MAUI.MVVM.Models;

public class PokemonTypeDefenseModel: ObservableObject
{
    public string Multiplier { get; set; }

    public EffectEnum Effect { get; set; }

    public TypeEnum Type { get; set; }

    private bool _isBusy;
    public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value); }
}