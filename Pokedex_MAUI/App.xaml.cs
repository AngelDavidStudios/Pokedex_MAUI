using LiteDB;
using Models.Login;
using Pokedex_MAUI.MVVM.Views;

namespace Pokedex_MAUI;

public partial class App : Application
{
    public static Users User;
    public static LiteDatabase Database = new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "pokedex.db"));

    public App()
    {
        InitializeComponent();
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