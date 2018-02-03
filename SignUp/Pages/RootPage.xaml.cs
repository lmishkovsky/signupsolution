using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SignUp.Pages
{
    /// <summary>
    /// Root page.
    /// </summary>
    public partial class RootPage : TabbedPage
    {
        /// <summary>
        /// Default parameterless constructor
        /// </summary>
        public RootPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor used from the App.xaml.cs class
        /// </summary>
        /// <param name="groupCode">Group code.</param>
        public RootPage(DateTime dtNextEventDate)
        {
            InitializeComponent();

            // remove the navigation bar at the top
            // NavigationPage.SetHasNavigationBar(this, false);

            LoginDependencyPage accountPage = new LoginDependencyPage(); // new AccountPage();
            //GroupCodePage groupCodePage = new GroupCodePage();
            ShowSignupsPage showSignupsPage = new ShowSignupsPage(dtNextEventDate);
            ForumPage forumPage = new ForumPage(dtNextEventDate);
            //SettingsPage settingsPage = new SettingsPage();

            Children.Add(showSignupsPage);
            Children.Add(forumPage);
            Children.Add(accountPage);
            //Children.Add(groupCodePage);
            //Children.Add(settingsPage);
        }
    }
}
