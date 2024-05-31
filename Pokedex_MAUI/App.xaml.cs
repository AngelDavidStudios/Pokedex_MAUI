using LiteDB;
using Pokedex_MAUI.MVVM.Views;

namespace Pokedex_MAUI;

public partial class App : Application
{
    public static LiteDatabase Database = new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "pokedex.db"));

    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage());

    }
}