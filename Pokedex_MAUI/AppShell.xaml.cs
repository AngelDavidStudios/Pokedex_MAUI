using Pokedex_MAUI.MVVM.ViewModels;
using Pokedex_MAUI.MVVM.Views;
using Pokedex_MAUI.MVVM.Views.ContentViews;

namespace Pokedex_MAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        BindingContext = new VMAppShell();
        Routing.RegisterRoute(nameof(HomeView), typeof(HomeView));
        Routing.RegisterRoute(nameof(AccountContentView), typeof(AccountContentView));
        Routing.RegisterRoute(nameof(LoginPageView), typeof(LoginPageView));
    }
}