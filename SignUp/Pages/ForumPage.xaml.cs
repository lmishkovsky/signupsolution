using System;
using System.Collections.Generic;
using Plugin.Settings;
using SignUp.ViewModels;
using Xamarin.Forms;

namespace SignUp.Pages
{
    public partial class ForumPage : ContentPage
    {
        DateTime dtNextEventDate;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Pages.ForumPage"/> class.
        /// </summary>
        public ForumPage() 
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Pages.ForumPage"/> class.
        /// </summary>
        /// <param name="dtNextEventDate">Dt next event date.</param>
        public ForumPage(DateTime dtNextEventDate)
        {
            this.dtNextEventDate = dtNextEventDate;

            InitializeComponent();

            Title = "Messages"; // string.Format("{0}", dtNextEventDate.ToLocalTime().ToString("dddd, dd MMM, H:mm"));
            Icon = "ic_message.png";

            BindingContext = new ForumPageViewModel(dtNextEventDate);
        }

        /// <summary>
        /// Buttons the clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public async void Button_Clicked(object sender, EventArgs e)
        {
            // await Application.Current.MainPage.DisplayAlert("FAB Clicked!", "Congrats on creating your FAB!", "Thanks!");

            string facebookID = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookID, string.Empty);

            if (String.IsNullOrEmpty(facebookID))
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Pages.LoginDependencyPage());
            }
            else
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Pages.NewMessage(dtNextEventDate));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var bindingContext = this.BindingContext as ForumPageViewModel;

            if (bindingContext == null) {
                return;
            }

            bindingContext.ExecuteRefreshCommand();
        }
    }
}
