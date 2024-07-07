using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Pokedex_MAUI.MVVM.ViewModels;

public partial class VMAppShell: ObservableObject
{
    [RelayCommand]
    async void SignOut()
    {
        if (Preferences.ContainsKey(nameof(App.User)))
        {
            Preferences.Remove(nameof(App.User));
        }
        await Shell.Current.GoToAsync("//LoginPageView");
    }
    
    [RelayCommand]
    async void CloseModal()
    {
        await Shell.Current.Navigation.PopModalAsync();
    }
}