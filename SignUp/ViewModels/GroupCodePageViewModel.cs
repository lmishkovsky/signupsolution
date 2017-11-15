using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Plugin.Settings;
using Signup.Helpers;
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

            GroupCode = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.GroupCode, string.Empty);
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
						// <Schedule><Entries><Entry><Start>2017-11-15T12:04:03.850294Z</Start><Repeat>Weekly</Repeat></Entry><Entry><Start>2017-11-16T12:04:05.947764Z</Start><Repeat>Custom</Repeat></Entry></Entries></Schedule>
						string scheduleAsString = list[0].Schedule;

                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Schedule));

						using (TextReader reader = new StringReader(scheduleAsString))
						{
                            Schedule schedule = (Schedule)xmlSerializer.Deserialize(reader);
						}

                        #region [Sample XML serialization]

                        //                  Schedule schedule = new Schedule();

                        //                  Signup.Helpers.Entry entry1 = new Signup.Helpers.Entry();
                        //                  entry1.Start = DateTime.UtcNow;
                        //                  entry1.Repeat = Helpers.RepeatEnum.Weekly;
                        //                  schedule.Entries.Add(entry1);

                        //                  Signup.Helpers.Entry entry2 = new Signup.Helpers.Entry();
                        //                  entry2.Start = DateTime.Now.AddDays(1).ToUniversalTime();
                        //                  entry2.Repeat = Helpers.RepeatEnum.Custom;
                        //                  schedule.Entries.Add((entry2));

                        //                  XmlSerializer xmlSerializer = new XmlSerializer(typeof(Schedule));

                        //using (StringWriter textWriter = new StringWriter())
                        //{
                        //                      xmlSerializer.Serialize(textWriter, schedule);
                        //	string xmlString = textWriter.ToString();
                        //}

#endregion

                        // save group code in user settings (so that user does not have to enter it every time)
                        CrossSettings.Current.AddOrUpdateValue(Constants.CrossSettingsKeys.GroupCode, GroupCode);

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
