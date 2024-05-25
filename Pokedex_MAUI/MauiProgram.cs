using Microsoft.Extensions.Logging;

namespace Pokedex_MAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
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