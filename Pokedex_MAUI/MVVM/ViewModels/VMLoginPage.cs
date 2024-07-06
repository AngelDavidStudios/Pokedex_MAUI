using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Models.Login;
using Newtonsoft.Json;
using Pokedex_MAUI.Services;

namespace Pokedex_MAUI.MVVM.ViewModels;

public partial class VMLoginPage: ObservableObject
{
    [ObservableProperty]
    private string _userName;

    [ObservableProperty]
    private string _password;

    readonly ILoginService loginService = new LoginService();
    
    [RelayCommand]
    public async void Login()
    {
        try {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet) {

                if (!string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password)) {
                    Users user = await loginService.Login(UserName, Password);
                    if (user == null) {
                        await Shell.Current.DisplayAlert("Error", "Username/Password is incorrect", "Ok");
                        return;
                    }
                    if (Preferences.ContainsKey(nameof(App.User))) {
                        Preferences.Remove(nameof(App.User));
                    }
                    string userDetails = JsonConvert.SerializeObject(user);
                    Preferences.Set(nameof(App.User), userDetails);
                    App.User = user;
                    await Shell.Current.GoToAsync("//HomeView");
                } else {
                    await Shell.Current.DisplayAlert("Error", "All fields required", "Ok");
                }
            } else {
                await Shell.Current.DisplayAlert("Error", "No Internet Access", "Ok");
            }
        } catch (Exception ex) {
            await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
    
    [RelayCommand]
    async Task ShowSignUpModal()
    {
        await Application.Current.MainPage.Navigation.PushModalAsync(new SignUpView());
    }
}