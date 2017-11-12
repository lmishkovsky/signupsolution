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
using Org.Json;

namespace SignUp.Droid
{
    [Activity(Label = "SignUp.Droid"
              , Icon = "@drawable/icon"
              , Theme = "@style/MyTheme"
              , MainLauncher = true
              , ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
              , Name = "solutions.brightsoft.signup.mainactivity"
             )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, GraphRequest.IGraphJSONObjectCallback
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

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

			#region [ Facebook ]

			FacebookSdk.SdkInitialize(ApplicationContext);

			callbackManager = CallbackManagerFactory.Create();

			var loginCallback = new FacebookCallback<LoginResult>
			{
				HandleSuccess = loginResult =>
				{
					var facebookToken = AccessToken.CurrentAccessToken.Token;

                    // Obtain the Facebook's user name and email 
                    GraphRequest graphRequest = GraphRequest.NewMeRequest(AccessToken.CurrentAccessToken, this);
                    Bundle parameters = new Bundle();
                    parameters.PutString("fields", "id,name,email");
                    graphRequest.Parameters = parameters;
                    graphRequest.ExecuteAsync();

					// Login to the Azure Mobile Service here
					//var token = new JObject();
					//token["access_token"] = facebookToken;
					//var user = await Client.LoginAsync(MobileServiceAuthenticationProvider.Facebook, token);

                    // Call the Facebook Graph API to obtains the user's name and email (if available) 

                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());
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

        void GraphRequest.IGraphJSONObjectCallback.OnCompleted(JSONObject json, GraphResponse response)
        {
            Console.WriteLine(json.ToString());
        }
    }
}
