using CommunityToolkit.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Pokedex_MAUI.Handlers;
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
        
        EntryBorderlessHandler.Configure();
        return builder.Build();
    }
}