using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pokedex_MAUI.MVVM.ViewModels;

namespace Pokedex_MAUI.MVVM.Views;

public partial class AccountView : ContentPage
{
    public AccountView()
    {
        InitializeComponent();
        if(App.User != null)
        {
            lblUserName.Text = App.User.FullName;
            lblUserEmail.Text = App.User.Email;
        }

        BindingContext = new VMAppShell();
    }
}