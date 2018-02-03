using Plugin.Settings;
using SignUp.Abstractions;
using SignUp.Pages;
using SignUp.Services;
using Xamarin.Forms;

#if __ANDROID__
using Xamarin.Facebook;
#endif

namespace SignUp
{
    /// <summary>
    /// App.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Needed to access Azure services
        /// </summary>
        /// <value>The cloud service.</value>
        public static ICloudService CloudService { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            CloudService = new AzureCloudService();

            MainPage = new NavigationPage(new GroupCodePage());

            //// try to obtain the group code from saved app settings
            //var groupCode = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.GroupCode, string.Empty);

            //if (string.IsNullOrEmpty(groupCode))
            //{
            //    // we don't know in what group context to run the app, so send to the group code page
            //    MainPage = new NavigationPage(new GroupCodePage());
            //}
            //else {
            //    // if we know in what group context to run the app, send to the root tabbed page
            //    MainPage = new NavigationPage(new RootPage(groupCode));
            //}
		}

        /// <summary>
        /// Ons the start.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        /// <summary>
        /// Ons the sleep.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Ons the resume.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
