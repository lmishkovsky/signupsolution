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

            var facebookID = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookID, string.Empty);

            // MainPage = new NavigationPage(new MessagesPage());

            MainPage = new NavigationPage(new RootPage());

            //if (string.IsNullOrEmpty(facebookID))
            //{
            //    MainPage = new NavigationPage(new LoginDependencyPage());
            //}
            //else {
            //    MainPage = new NavigationPage(new GroupCodePage());
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
