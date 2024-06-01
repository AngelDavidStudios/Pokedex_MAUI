using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokedex_MAUI.MVVM.ViewModels;
using Pokedex_MAUI.Services;

namespace Pokedex_MAUI.MVVM.Views;

public partial class HomeView : ContentPage
{
    public HomeView()
    {
        InitializeComponent();
        BindingContext = new VMHome(Navigation, new RestService());
    }
}