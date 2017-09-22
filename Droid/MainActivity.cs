using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using SignUp.Droid.FacebookHelper;
using Xamarin.Facebook.Login;

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
        ICallbackManager callbackManager;

        /// <summary>
        /// Ons the create.
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			#region [ Facebook ]

			FacebookSdk.SdkInitialize(ApplicationContext);

			callbackManager = CallbackManagerFactory.Create();

			var loginCallback = new FacebookCallback<LoginResult>
			{
				HandleSuccess = loginResult =>
				{

					var facebookToken = AccessToken.CurrentAccessToken.Token;
					//Login here
				},
				HandleCancel = () =>
				{
					//Handle Cancel  
				},
				HandleError = loginError =>
				{
					//Handle Error        
				}
			};

            LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);

			#endregion

			LoadApplication(new App());
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

			callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
		}
    }
}
