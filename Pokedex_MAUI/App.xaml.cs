using LiteDB;
namespace Pokedex_MAUI;

public partial class App : Application
{
    public static LiteDatabase Database = new LiteDatabase(Path.Combine(FileSystem.AppDataDirectory, "pokedex.db"));

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}