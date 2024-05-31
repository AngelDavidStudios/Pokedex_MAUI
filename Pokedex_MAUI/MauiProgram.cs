using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Pokedex_MAUI.Interfaces;
using Pokedex_MAUI.MVVM.Models;
using Pokedex_MAUI.MVVM.ViewModels;
using Pokedex_MAUI.MVVM.Views;
using Pokedex_MAUI.Services;
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("sf-pro-display-regular.ttf", "FontRegular");
                fonts.AddFont("sf-pro-display-medium.ttf", "FontMedium");
                fonts.AddFont("sf-pro-display-bold.ttf", "FontBold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}