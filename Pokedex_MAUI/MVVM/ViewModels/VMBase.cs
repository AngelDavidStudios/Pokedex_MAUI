using Pokedex_MAUI.ObservableCollections;

namespace Pokedex_MAUI.MVVM.ViewModels;

public class VMBase: ObservableObject
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

    private bool _isBusy;
    public bool IsBusy
    {
        get { return _isBusy; }
        set
        {
            SetProperty(ref _isBusy, value);
        }
    }
}