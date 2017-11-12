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
		ObservableCollection<GroupItem> items = new ObservableCollection<GroupItem>();

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
		public ObservableCollection<GroupItem> Items
		{
			get { return items; }
			set { SetProperty(ref items, value, "Items"); }
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.ViewModels.ShowSignupsPageViewModel"/> class.
        /// </summary>
        public ShowSignupsPageViewModel()
        {
            Title = "Signups";

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
                var table = App.CloudService.GetTable<GroupItem>();
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
    }
}
