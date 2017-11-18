using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SignUp.CustomControls;
using SignUp.iOS.CustomRenderers;
using Facebook.CoreKit;
using Facebook.LoginKit;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Plugin.Settings;

[assembly: ExportRenderer(typeof(FacebookLoginButton), typeof(FacebookLoginButtonRendererIos))]

namespace SignUp.iOS.CustomRenderers
{
	public class FacebookLoginButtonRendererIos : ButtonRenderer
	{

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				UIButton button = Control;

				button.TouchUpInside += delegate
				{
					HandleFacebookLoginClicked();
				};
			}

			if (AccessToken.CurrentAccessToken != null)
			{
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());
			}
		}

		void HandleFacebookLoginClicked()
		{
			if (AccessToken.CurrentAccessToken != null)
			{
                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());
			}
			else
			{
				var window = UIApplication.SharedApplication.KeyWindow;
				var vc = window.RootViewController;
				while (vc.PresentedViewController != null)
				{
					vc = vc.PresentedViewController;
				}

				var manager = new LoginManager();
				manager.LogInWithReadPermissions(new string[] { "public_profile", "email" },
												 vc,
												 (result, error) =>
												 {
													 if (error == null && !result.IsCancelled)
													 {                       
                                                        // request user's name and email from Facebook
                                                        string data = GetFacebookData(result.Token.TokenString); 

                                                        // deserialize data into a JSON object
                                                        var facebookData = JObject.Parse(data.ToString());
                                                            
                                                         // save settings
														 CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookID, facebookData[SignUp.Constants.FacebookAttributes.ID] != null ? facebookData[SignUp.Constants.FacebookAttributes.ID].ToString() : string.Empty);
														 CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookName, facebookData[SignUp.Constants.FacebookAttributes.Name] != null ? facebookData[SignUp.Constants.FacebookAttributes.Name].ToString() : string.Empty);
														 CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookEmail, facebookData[SignUp.Constants.FacebookAttributes.Email] != null ? facebookData[SignUp.Constants.FacebookAttributes.Email].ToString() : string.Empty);

														 // App.PostSuccessFacebookAction(AccessToken.CurrentAccessToken.TokenString);

														 // Go to the group code page once logged in
														 Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());
													 }
												 });
			}

		}

        /// <summary>
        /// Sends the sms.
        /// </summary>
        /// <returns>The sms.</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="to">To.</param>
        /// <param name="message">Message.</param>
        /// <param name="originator">Originator.</param>
		public static string GetFacebookData(string facebookToken)
		{
			StringBuilder sb = new StringBuilder();
			byte[] buf = new byte[1024];
			string url = "https://graph.facebook.com/me" +
				"?fields=name,email" +
				"&access_token=" + facebookToken;
            
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream resStream = response.GetResponseStream();
			string tempString = null;
			int count = 0;
			do
			{
				count = resStream.Read(buf, 0, buf.Length);
				if (count != 0)
				{
					tempString = Encoding.ASCII.GetString(buf, 0, count);
					sb.Append(tempString);
				}
			}
			while (count > 0);
			return sb.ToString();
		}

	}
}
