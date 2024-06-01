using Pokedex_MAUI.Controls;

namespace Pokedex_MAUI.Handlers;

public static class EntryBorderlessHandler
{
    public static void Configure()
    {
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(CustomEntryBorderless), (handler, view) =>
        {
            if (view is CustomEntryBorderless)
            {
#if IOS
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });
    }
}