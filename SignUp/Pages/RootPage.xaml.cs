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

            AccountPage accountPage = new AccountPage();
            //GroupCodePage groupCodePage = new GroupCodePage();
            ShowSignupsPage showSignupsPage = new ShowSignupsPage(DateTime.Now);
            ForumPage forumPage = new ForumPage(DateTime.Now);
            SettingsPage settingsPage = new SettingsPage();

            Children.Add(accountPage);
            //Children.Add(groupCodePage);
            Children.Add(showSignupsPage);
            Children.Add(forumPage);
            Children.Add(settingsPage);
        }
    }
}
