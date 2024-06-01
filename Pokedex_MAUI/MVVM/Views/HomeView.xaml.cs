using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokedex_MAUI.MVVM.ViewModels;
using Pokedex_MAUI.Services;

namespace Pokedex_MAUI.MVVM.Views;

public partial class HomeView : ContentPage
{
    const uint DURATION_ANIMATION = 1000;
    public HomeView()
    {
        InitializeComponent();
        BindingContext = new VMHome(Navigation, new RestService());
        
        labelTitle.TranslationX = -350;
        borderFilterGeneration.TranslationY = -300;
        borderFilterSort.TranslationY = -300;
        borderFilters.TranslationY = -300;
        customSearchBar.TranslationX = -300;
        collectionPokemons.Opacity = 0;
    }
    
    protected override async void OnAppearing()
    {
        await Task.Delay(2000);

        await Task.WhenAll(
            labelTitle.TranslateTo(0, -300, DURATION_ANIMATION, Easing.Linear),
            labelTitle.TranslateTo(0, -150, DURATION_ANIMATION, Easing.Linear),
            labelTitle.TranslateTo(0, -75, DURATION_ANIMATION, Easing.Linear),
            labelTitle.TranslateTo(0, 0, DURATION_ANIMATION, Easing.Linear),
            customSearchBar.TranslateTo(0, -300, DURATION_ANIMATION, Easing.Linear),
            customSearchBar.TranslateTo(0, -150, DURATION_ANIMATION, Easing.Linear),
            customSearchBar.TranslateTo(0, 0, DURATION_ANIMATION, Easing.Linear)
        );

        await Task.WhenAll(
            borderFilterGeneration.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
            borderFilterGeneration.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
            borderFilterGeneration.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
        );

        await Task.WhenAll(
            borderFilterSort.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
            borderFilterSort.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
            borderFilterSort.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
        );

        await Task.WhenAll(
            borderFilters.TranslateTo(0, -300, DURATION_ANIMATION, Easing.BounceOut),
            borderFilters.TranslateTo(0, -150, DURATION_ANIMATION, Easing.BounceOut),
            borderFilters.TranslateTo(0, 0, DURATION_ANIMATION, Easing.BounceOut)
        );

        await Task.WhenAll(
            collectionPokemons.FadeTo(0.5, DURATION_ANIMATION, Easing.Linear),
            collectionPokemons.FadeTo(1, DURATION_ANIMATION, Easing.Linear)
        );

        base.OnAppearing();
    }
}