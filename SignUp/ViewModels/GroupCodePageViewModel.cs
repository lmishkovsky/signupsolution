using System;
using System.Collections.Generic;
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

                        DateTime dtNextEventDate = DateTime.MinValue;

                        // deserialize into a Schedule object and obtain the next event date
						using (TextReader reader = new StringReader(scheduleAsString))
						{
                            Schedule schedule = (Schedule)xmlSerializer.Deserialize(reader);

                            dtNextEventDate = GetNextEventDate(schedule);
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

                        Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new Pages.ShowSignupsPage(dtNextEventDate));

                        // navigate to next page
                        // Application.Current.MainPage = new NavigationPage(new Pages.ShowSignupsPage(dtNextEventDate));
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

        /// <summary>
        /// Gets the next event date.
        /// </summary>
        /// <returns>The next event date.</returns>
        private DateTime GetNextEventDate(Schedule schedule) 
        {
            // this is the return value
            DateTime retEventDate = DateTime.MinValue;

            // this is the current date and time
            DateTime dtUtcNow = DateTime.UtcNow;

            // a temp working variable
            DateTime dtTemp = DateTime.MinValue;

            // this is the list of date time entries in the schedule
            // we will be increasing their values according to the Repeat model (daily, weekly)
            List<DateTime> listLastProcessedDatesOriginals = new List<DateTime>();

			// collect all start dates we are going to compare the current time against
			foreach (Signup.Helpers.Entry e in schedule.Entries)
			{
                listLastProcessedDatesOriginals.Add(e.Start);
			}

            // create an array to store temporary incremented values
            DateTime[] listLastProcessedDatesCopy = new DateTime[listLastProcessedDatesOriginals.Count];

            // while the return date is not set, keep looping
            while (retEventDate == DateTime.MinValue)
            {
                listLastProcessedDatesOriginals.CopyTo(listLastProcessedDatesCopy);

                // start incrementing the initial dates until we find a good match
                for (int i = 0; i < listLastProcessedDatesCopy.Length; i++)
                {
                    dtTemp = listLastProcessedDatesCopy[i];

                    // if the next scheduled date is in the future
                    if (dtUtcNow < dtTemp)
                    {
                        // use this value and return
                        retEventDate = dtTemp;
                        break;
                    }
                    else 
                    {
                        // TODO: We are assuming that the Repeat is set to "Weekly" but it can be: Never, Daily, Weekly, Monthly, Yearly,Custom
                        listLastProcessedDatesOriginals[i] = dtTemp.AddDays(7);
                    }
                }
            }

            return retEventDate;
        }
    }
}
