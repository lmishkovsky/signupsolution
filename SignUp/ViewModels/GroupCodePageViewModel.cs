using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SignUp.Abstractions;
using SignUp.Models;
using Xamarin.Forms;

namespace SignUp.ViewModels
{
    /// <summary>
    /// Group code page view model.
    /// </summary>
    public class GroupCodePageViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.ViewModels.GroupCodePageViewModel"/> class.
        /// </summary>
        public GroupCodePageViewModel()
        {
            Title = "Group Code";
        }

        Command btnCommand;

        public string GroupCode { get; set; }

        public Command GroupCheckCommand => btnCommand ?? (btnCommand = new Command(async () => ExecuteGroupCheckCommand()));

        async Task ExecuteGroupCheckCommand()
        {
            if (IsBusy) return;

            IsBusy = true;

            try 
            {
                // if group code was passed from the user
                if (!String.IsNullOrEmpty(GroupCode))
                {
                    // get a reference to the group codes table
                    var table = App.CloudService.GetTable<GroupItem>();

                    // query for this group code
                    var list = await table.GetTheMobileServiceTable().Where(groupItem => groupItem.GroupCode == GroupCode).ToListAsync();

                    // if group code found
                    if (list != null && list.Count > 0)
                    {
                        // navigate to next page
                        Application.Current.MainPage = new NavigationPage(new Pages.ShowSignupsPage());
                    }
                    else {
                        //await DisplayAlert("Alert", "You have been alerted", "OK");
                        await Application.Current.MainPage.DisplayAlert("Alert", "No such group! Try a different group code. Group codes are case sensitive.", "OK");
                    }
                }
				else
				{
					//await DisplayAlert("Alert", "You have been alerted", "OK");
					await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a group code.", "OK");
				}
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
