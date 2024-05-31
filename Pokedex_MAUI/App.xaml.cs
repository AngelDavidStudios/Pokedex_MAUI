using LiteDB;
using Pokedex_MAUI.MVVM.Views;

namespace Pokedex_MAUI;

public partial class App : Application
{
    public static LiteDatabase Database;

    public App()
    {
        InitializeComponent();
        Database = new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "pokedex.db"));
        MainPage = new NavigationPage(new HomeView());
    }
    
    protected override void OnStart()
    {
    }

    protected override void OnSleep()
    {
    }

    protected override void OnResume()
    {
    }
}