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

            Title = "Messages"; // string.Format("{0}", dtNextEventDate.ToLocalTime().ToString("dddd, dd MMM, H:mm"));
            Icon = "ic_message.png";

            BindingContext = new ForumPageViewModel(dtNextEventDate);
        }
    }
}
