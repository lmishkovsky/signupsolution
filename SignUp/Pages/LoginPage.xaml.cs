using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SignUp.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

			App.PostSuccessFacebookAction = token =>
			{
                
			};
        }
    }
}
