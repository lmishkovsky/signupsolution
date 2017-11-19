using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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
        DateTime dtNextEventDate;

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
            this.dtNextEventDate = dtNextEventDate;

            Title = string.Format("{0}", dtNextEventDate.ToLocalTime().ToString("dddd, dd MMM yyyy, H:mm"));

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
            Contract.Ensures(Contract.Result<Task>() != null);
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await BindSignupsList();
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

        async Task BindSignupsList()
        {
            var table = App.CloudService.GetTable<SignupItem>();

            // query for this event
            var list = await table.GetTheMobileServiceTable()
                                  .Where(signupItem => signupItem.GroupCode == this.groupCode && signupItem.EventDate == this.dtNextEventDate.ToLocalTime())
                                  .OrderBy(signupItem => signupItem.UpdatedAt)
                                  .ToListAsync();

            Items.Clear();
            foreach (var item in list)
            {
                item.Index = string.Format("{0}. ", (list.IndexOf(item) + 1).ToString());

                item.UpdatedAtAsString = string.Format("{0:ddd, dd MMM, hh:mm}", item.UpdatedAt); // item.UpdatedAt.ToString();

                Items.Add(item);
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
				var table = App.CloudService.GetTable<SignupItem>();

				// query for this event and user
				var list = await table.GetTheMobileServiceTable().
                                      Where(signupItem => signupItem.GroupCode == this.groupCode 
                                            && signupItem.EventDate == this.dtNextEventDate.ToLocalTime()
                                            && signupItem.UserID == this.facebookID
                                           ).ToListAsync();

                if (list != null && list.Count > 0)
                {
                    // show message user is already in the list
                    await Application.Current.MainPage.DisplayAlert("Alert", "You are already in the list.", "OK");
                }
                else
                {
                    // prepare the new entry 
                    SignupItem newSignup = new SignupItem();
                    newSignup.GroupCode = this.groupCode;
                    newSignup.UserID = this.facebookID;
                    newSignup.Name = this.facebookName;
                    newSignup.Email = this.facebookEmail;
                    newSignup.EventDate = this.dtNextEventDate;

                    // add the new signup
                    await table.CreateItemAsync(newSignup);
                }

                // refresh list
                await BindSignupsList();
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
