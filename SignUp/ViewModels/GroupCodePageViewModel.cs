using System;
using System.Diagnostics;
using System.Threading.Tasks;
using SignUp.Abstractions;
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

        public Command GroupCheckCommand => btnCommand ?? (btnCommand = new Command(async () => ExecuteGroupCheckCommand()));

        async Task ExecuteGroupCheckCommand()
        {
            if (IsBusy) return;

            IsBusy = true;

            try 
            {
                // check group code and move to next page
                Application.Current.MainPage = new NavigationPage(new Pages.ShowSignupsPage());
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
