using System;
using System.Collections.Generic;
using SignUp.ViewModels;
using Xamarin.Forms;

namespace SignUp.Pages
{
    /// <summary>
    /// Group code page.
    /// </summary>
    public partial class GroupCodePage : ContentPage
    {
        public GroupCodePage()
        {
            InitializeComponent();

            Title = "Groups";
            Icon = "ic_event";

            // make the connection between the page and the view model
            BindingContext = new GroupCodePageViewModel();
        }
    }
}
