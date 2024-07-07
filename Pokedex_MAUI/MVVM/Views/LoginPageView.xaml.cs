using Pokedex_MAUI.MVVM.ViewModels;

namespace Pokedex_MAUI.MVVM.Views;

public partial class LoginPageView : ContentPage
{
    public LoginPageView()
    {
        InitializeComponent();
        BindingContext = new VMLoginPage();
    }
}