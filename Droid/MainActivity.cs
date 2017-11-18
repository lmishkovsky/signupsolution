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
using Plugin.Settings;
using Newtonsoft.Json.Linq;

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

                    // navigate to the 2nd (Group Code) page if logged in
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
            var facebookData = JObject.Parse(json.ToString());

            CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookID, facebookData[SignUp.Constants.FacebookAttributes.ID] != null ? facebookData[SignUp.Constants.FacebookAttributes.ID].ToString() : string.Empty);
            CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookName, facebookData[SignUp.Constants.FacebookAttributes.Name] != null ? facebookData[SignUp.Constants.FacebookAttributes.Name].ToString() : string.Empty);
            CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookEmail, facebookData[SignUp.Constants.FacebookAttributes.Email] != null ? facebookData[SignUp.Constants.FacebookAttributes.Email].ToString() : string.Empty);
        }
    }
}
