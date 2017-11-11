using SignUp.Abstractions;
using SignUp.Pages;
using SignUp.Services;
using Xamarin.Facebook;
//using Xamarin.Facebook;
using Xamarin.Forms;

namespace SignUp
{
    /// <summary>
    /// App.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets or sets the post success facebook action - USED FROM iOS and not Android client app.
        /// </summary>
        /// <value>The post success facebook action.</value>
        public static System.Action<string> PostSuccessFacebookAction { get; set; }

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

			// AccessToken.CurrentAccessToken is set by the login process for Android
			// PostSuccessFacebookAction is set by the login process for iOS
			if (AccessToken.CurrentAccessToken != null || PostSuccessFacebookAction != null)
            {
                // go directly to the group code page if used is already logged in
                MainPage = new NavigationPage(new GroupCodePage());
            }
            else 
            {
                // stay on the login page if user is not logged in
                MainPage = new NavigationPage(new LoginPage());
            }
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
