using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Xamarin.Facebook;
using SignUp.Droid.Dependency;

namespace SignUp.Droid
{
    [Activity(Label = "SignUp.Droid"
              , Icon = "@drawable/icon"
              , Theme = "@style/MyTheme"
              , MainLauncher = true
              , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
              , Name = "solutions.brightsoft.signup.mainactivity"
             )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// Ons the create.
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
			FacebookSdk.SdkInitialize(ApplicationContext);
			LoadApplication(new App());
        }

		protected override void OnResume()
		{
			base.OnResume();
            Xamarin.Facebook.AppEvents.AppEventsLogger.ActivateApp(Application);
		}

        /// <summary>
        /// Ons the activity result.
        /// </summary>
        /// <param name="requestCode">Request code.</param>
        /// <param name="resultCode">Result code.</param>
        /// <param name="data">Data.</param>
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			AndroidFacebookService.Instance.OnActivityResult(requestCode, (int)resultCode, data);
		}
    }
}
