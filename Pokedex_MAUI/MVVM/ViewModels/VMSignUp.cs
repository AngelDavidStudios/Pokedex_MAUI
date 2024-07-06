using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pokedex_MAUI.Services;

namespace Pokedex_MAUI.MVVM.ViewModels;

public partial class VMSignUp: ObservableObject
{
    [ObservableProperty]
    private string fullName;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string confirmPassword;

    [ObservableProperty]
    private string message;

    private readonly ILoginService loginService;
    
    public VMSignUp()
    {
        loginService = new LoginService();
    }
    
    [RelayCommand]
    public async Task Register()
    {
        if (string.IsNullOrWhiteSpace(FullName) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
        {
            Message = "Please fill in all fields.";
            return;
        }

        if (Password != ConfirmPassword)
        {
            Message = "Passwords do not match.";
            return;
        }
        var success = await loginService.Register(FullName, Email, Password);
        if (success != null)
        {
            Message = "Registration successful.";
            await Application.Current.MainPage.DisplayAlert("Success", "Usuario Registrado con exito.", "Ok");
            await Application.Current.MainPage.Navigation.PopModalAsync();

        }
        else
        {
            Message = "Registration failed.";
            await Application.Current.MainPage.DisplayAlert("Error", "Error al registrar usuario.", "Ok");
        }
    }
}