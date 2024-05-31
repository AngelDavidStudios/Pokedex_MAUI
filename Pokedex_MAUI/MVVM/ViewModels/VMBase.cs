using PropertyChanged;

namespace Pokedex_MAUI.MVVM.ViewModels;

[AddINotifyPropertyChangedInterface]
public class VMBase
{
    public INavigation Navigation;
    
    public VMBase(INavigation navigation)
    {
        Navigation = navigation;
    }

    public virtual void OnDisappearing() { }
    
    public static bool InternetConnectivity()
    {
        var connectivity = Connectivity.NetworkAccess;
        if (connectivity == NetworkAccess.Internet)
            return true;

        return false;
    }
    
    public bool IsBusy { get; set; }
}