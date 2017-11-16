using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Settings;
using SignUp.Abstractions;
using SignUp.Models;
using Xamarin.Forms;

namespace SignUp.ViewModels
{
    /// <summary>
    /// Show signups page view model.
    /// </summary>
    public class ShowSignupsPageViewModel : BaseViewModel
    {
		string facebookID = string.Empty;
		string facebookName = string.Empty;
		string facebookEmail = string.Empty;
        string groupCode = string.Empty;

		/// <summary>
		/// The items.
		/// </summary>
		ObservableCollection<SignupItem> items = new ObservableCollection<SignupItem>();

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public ObservableCollection<SignupItem> Items
		{
			get { return items; }
			set { SetProperty(ref items, value, "Items"); }
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.ViewModels.ShowSignupsPageViewModel"/> class.
        /// </summary>
        public ShowSignupsPageViewModel(DateTime dtNextEventDate)
        {
            Title = string.Format("{0}", dtNextEventDate.ToLocalTime().ToString("dddd, dd/MMM/yyyy, H:mm"));

            this.facebookID = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookID, string.Empty);
            this.facebookName = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookName, string.Empty);
            this.facebookEmail = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookEmail, string.Empty);
            this.groupCode = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.GroupCode, string.Empty);

            RefreshList();
        }

        /// <summary>
        /// Refreshs the list.
        /// </summary>
        /// <returns>The list.</returns>
		async Task RefreshList()
		{
			await ExecuteRefreshCommand();

			//MessagingCenter.Subscribe<TaskDetailViewModel>(this, "ItemsChanged", async (sender) =>
			//{
			//	await ExecuteRefreshCommand();
			//});
		}

		Command refreshCmd;
		public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

		/// <summary>
		/// Executes the refresh command.
		/// </summary>
		/// <returns>The refresh command.</returns>
		async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;
			IsBusy = true;

			try
			{
                var table = App.CloudService.GetTable<SignupItem>();
                var list = await table.ReadAllItemsAsync();

                Items.Clear();
				foreach (var item in list)
					Items.Add(item);
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"[TaskList] Error loading items: {ex.Message}");
			}
			finally
			{
				IsBusy = false;
			}
		}

		Command btnCommand;

		public Command AddNewItemCommand => btnCommand ?? (btnCommand = new Command(async () => ExecuteAddNewItemCommand()));

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
                SignupItem newSignup = new SignupItem();
                newSignup.GroupCode = this.groupCode;
                newSignup.UserID = this.facebookID;
                newSignup.Name = this.facebookName;
                newSignup.Email = this.facebookEmail;
                newSignup.EventDate = DateTime.Now;

				var table = App.CloudService.GetTable<SignupItem>();
                await table.CreateItemAsync(newSignup);

				var list = await table.ReadAllItemsAsync();

				Items.Clear();
				foreach (var item in list)
					Items.Add(item);
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
