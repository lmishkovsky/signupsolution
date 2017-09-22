using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using SignUp.CustomControls;
using SignUp.iOS.CustomRenderers;
using Facebook.CoreKit;
using Facebook.LoginKit;

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
				App.PostSuccessFacebookAction(AccessToken.CurrentAccessToken.ToString());
			}
		}

		void HandleFacebookLoginClicked()
		{
			if (AccessToken.CurrentAccessToken != null)
			{
				App.PostSuccessFacebookAction(AccessToken.CurrentAccessToken.ToString());
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
														 App.PostSuccessFacebookAction(result.Token.ToString());
													 }
												 });
			}

		}
	}
}
