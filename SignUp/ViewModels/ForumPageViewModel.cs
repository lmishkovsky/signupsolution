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
    /// Forum page view model.
    /// </summary>
    public class ForumPageViewModel : BaseViewModel
    {
        string facebookID = string.Empty;
        string facebookName = string.Empty;
        string facebookEmail = string.Empty;
        string groupCode = string.Empty;
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

        string nextEventDateString;
        public string NextEventDateString
        {
            get
            {
                return "Next event: " + this.dtNextEventDate.ToLocalTime().ToString("ddd, dd MMM, HH:mm");
            }
            set
            {
                nextEventDateString = value;
                OnPropertyChanged("NextEventDateString");
            }
        }

        /// <summary>
        /// The items.
        /// </summary>
        ObservableCollection<ForumItem> items = new ObservableCollection<ForumItem>();

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public ObservableCollection<ForumItem> Items
        {
            get { return items; }
            set { SetProperty(ref items, value, "Items"); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.ViewModels.ForumPageViewModel"/> class.
        /// </summary>
        public ForumPageViewModel(DateTime dtNextEventDate)
        {
            this.facebookID = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookID, string.Empty);
            this.facebookName = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookName, string.Empty);
            this.facebookEmail = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookEmail, string.Empty);
            this.groupCode = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.GroupCode, string.Empty);

            this.dtNextEventDate = dtNextEventDate;

            ExecuteRefreshCommand();
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

                // add the new signup
                await table.CreateItemAsync(newPost);

                // show new forum post on top
                await BindForumList();

                this.Message = string.Empty;
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

        /// <summary>
        /// Binds the forum list.
        /// </summary>
        /// <returns>The forum list.</returns>
		async Task BindForumList()
        {
            var table = App.CloudService.GetTable<ForumItem>();

            // query for this event
            var list = await table.GetTheMobileServiceTable()
                                  .Where(forumItem => forumItem.GroupCode == this.groupCode && forumItem.EventDate == this.dtNextEventDate.ToLocalTime())
                                  .OrderByDescending(forumItem => forumItem.CreatedAt)
                                  .ToListAsync();

            Items.Clear();

            foreach (var item in list)
            {
                item.Index = string.Format("{0}. ", (list.IndexOf(item) + 1).ToString());

                item.UpdatedAtAsString = string.Format("{0:dd MMM, HH:mm}", item.CreatedAt);

                //if (item.UserID.Equals(this.facebookID))
                //{
                //	this.ButtonText = REMOVEME;
                //}

                Items.Add(item);
            }
        }

        Command refreshCmd;
        public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

        /// <summary>
        /// Executes the refresh command.
        /// </summary>
        /// <returns>The refresh command.</returns>
        public async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                await BindForumList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ForumList] Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }


    }
}
