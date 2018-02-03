using System;
using System.Collections.Generic;
using SignUp.ViewModels;
using Xamarin.Forms;

namespace SignUp.Pages
{
    public partial class NewMessage : ContentPage
    {
        public NewMessage(DateTime dtNextEventDate)
        {
            InitializeComponent();

            BindingContext = new NewMessagePageViewModel(dtNextEventDate);
        }
    }
}
