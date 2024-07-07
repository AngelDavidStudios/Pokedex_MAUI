using Pokedex_MAUI.MVVM.ViewModels;

namespace Pokedex_MAUI.MVVM.Views;

public partial class SignUpView : ContentPage
{
    public SignUpView()
    {
        InitializeComponent();
        BindingContext = new VMSignUp();
    }
}