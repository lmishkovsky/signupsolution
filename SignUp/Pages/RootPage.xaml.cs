using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace SignUp.Pages
{
    public partial class RootPage : TabbedPage
    {
        public RootPage()
        {
            InitializeComponent();

            // remove the navigation bar at the top
            // NavigationPage.SetHasNavigationBar(this, false);

            LoginDependencyPage accountPage = new LoginDependencyPage(); // new AccountPage();
            GroupCodePage groupCodePage = new GroupCodePage();
            ShowSignupsPage showSignupsPage = new ShowSignupsPage(DateTime.Now);
            ForumPage forumPage = new ForumPage(DateTime.Now);
            //SettingsPage settingsPage = new SettingsPage();

            Children.Add(accountPage);
            Children.Add(groupCodePage);
            Children.Add(showSignupsPage);
            Children.Add(forumPage);
            //Children.Add(settingsPage);
        }
    }
}
