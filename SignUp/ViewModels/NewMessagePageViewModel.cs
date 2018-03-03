using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Settings;
using SignUp.Abstractions;
using SignUp.Models;
using Xamarin.Forms;

namespace SignUp.ViewModels
{
    /// <summary>
    /// New message page view model.
    /// </summary>
    public class NewMessagePageViewModel : BaseViewModel
    {
        string facebookID = string.Empty;
        string facebookName = string.Empty;
        string facebookEmail = string.Empty;
        string groupCode = string.Empty;
        string imageUrl = string.Empty;
        DateTime dtNextEventDate;

        string message = "Type your message here.";
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.ViewModels.NewMessagePageViewModel"/> class.
        /// </summary>
        public NewMessagePageViewModel(DateTime dtNextEventDate)
        {
            this.facebookID = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookID, string.Empty);
            this.facebookName = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookName, string.Empty);
            this.facebookEmail = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookEmail, string.Empty);
            this.groupCode = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.GroupCode, string.Empty);
            this.imageUrl = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookImage, string.Empty);

            this.dtNextEventDate = dtNextEventDate;
        }

        Command btnCommand;

        public Command AddNewItemCommand => btnCommand ?? (btnCommand = new Command(async () => await ExecuteAddNewItemCommand()));

        /// <summary>
        /// Executes the add new item command.
        /// </summary>
        /// <returns>The add new item command.</returns>
        async Task ExecuteAddNewItemCommand()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                var table = App.CloudService.GetTable<ForumItem>();

                // prepare the new entry 
                ForumItem newPost = new ForumItem();
                newPost.GroupCode = this.groupCode;
                newPost.UserID = this.facebookID;
                newPost.Name = this.facebookName;
                newPost.Email = this.facebookEmail;
                newPost.EventDate = this.dtNextEventDate;
                newPost.Message = this.Message;
                newPost.ImageUrl = this.imageUrl;

                // add the new signup
                await table.CreateItemAsync(newPost);

                this.Message = string.Empty;

                // go back to previous page
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[GroupCheck] Error = {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
