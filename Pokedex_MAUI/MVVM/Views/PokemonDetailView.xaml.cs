using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokedex_MAUI.MVVM.Models;
using Pokedex_MAUI.MVVM.ViewModels;
using Pokedex_MAUI.Services;

namespace Pokedex_MAUI.MVVM.Views;

public partial class PokemonDetailView : ContentPage
{
    const uint DURATION_ANIMATION_IMAGE = 300;
    const uint DURATION_ANIMATION = 1000;
    public PokemonDetailView(PokemonModel pokemon)
    {
        InitializeComponent();
        BindingContext = new VMPokemonDetail(Navigation, new RestService(), pokemon);
        
        imagePokemon.TranslationX = -300;
        contentBadgeType.TranslationY = -300;
        labelPokemonNumberName.TranslationY = -300;
    }
    
    
    protected override async void OnAppearing()
    {
        await imagePokemon.TranslateTo(-300, 0, DURATION_ANIMATION_IMAGE, Easing.Linear);
        await imagePokemon.FadeTo(0.5, DURATION_ANIMATION_IMAGE, Easing.Linear);
        await imagePokemon.TranslateTo(-150, 0, DURATION_ANIMATION_IMAGE, Easing.Linear);
        await imagePokemon.TranslateTo(0, 0, DURATION_ANIMATION_IMAGE, Easing.Linear);
        await imagePokemon.FadeTo(1, DURATION_ANIMATION_IMAGE, Easing.Linear);

        await Task.WhenAll(
            contentBadgeType.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
            contentBadgeType.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
            contentBadgeType.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
        );

        await Task.WhenAll(
            labelPokemonNumberName.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
            labelPokemonNumberName.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
            labelPokemonNumberName.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
        );

        base.OnAppearing();
    }
}