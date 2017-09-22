using SignUp.Pages;
using Xamarin.Forms;

namespace SignUp
{
    /// <summary>
    /// App.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets or sets the post success facebook action.
        /// </summary>
        /// <value>The post success facebook action.</value>
        public static System.Action<string> PostSuccessFacebookAction { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
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
