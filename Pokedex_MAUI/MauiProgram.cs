using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Pokedex_MAUI.Handlers;
using Pokedex_MAUI.MVVM.ViewModels;
using Pokedex_MAUI.MVVM.Views;
using The49.Maui.BottomSheet;

namespace Pokedex_MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseBottomSheet()
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCompatibility()
            .ConfigureMauiHandlers(handlers =>
            {

            })
            .ConfigureEffects(effects =>
            {

            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("sf-pro-display-regular.ttf", "FontRegular");
                fonts.AddFont("sf-pro-display-medium.ttf", "FontMedium");
                fonts.AddFont("sf-pro-display-bold.ttf", "FontBold");
            });
        
        builder.Services.AddSingleton<HomeView>();
        builder.Services.AddSingleton<LoginPageView>();
        builder.Services.AddSingleton<SignUpView>();
        builder.Services.AddSingleton<PokemonDetailView>();
        builder.Services.AddSingleton<VMLoginPage>();
        builder.Services.AddSingleton<VMHome>();
        builder.Services.AddSingleton<VMSignUp>();
        builder.Services.AddSingleton<VMPokemonDetail>();
        builder.Services.AddSingleton<VMBase>();
        
        EntryBorderlessHandler.Configure();
        return builder.Build();
    }
}