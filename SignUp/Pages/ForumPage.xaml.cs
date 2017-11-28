using System;
using System.Collections.Generic;
using SignUp.ViewModels;
using Xamarin.Forms;

namespace SignUp.Pages
{
    public partial class ForumPage : ContentPage
    {
        public ForumPage(DateTime dtNextEventDate)
        {
            InitializeComponent();

			Title = string.Format("{0}", dtNextEventDate.ToLocalTime().ToString("dddd, dd MMM, H:mm"));

            BindingContext = new ForumPageViewModel(dtNextEventDate);
        }
    }
}
