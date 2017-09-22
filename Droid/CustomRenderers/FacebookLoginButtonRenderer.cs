using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SignUp.CustomControls.FacebookLoginButton), typeof(SignUp.Droid.CustomRenderers.FacebookLoginButtonRenderer))]
namespace SignUp.Droid.CustomRenderers
{
    /// <summary>
    /// Facebook login button renderer.
    /// </summary>
    public class FacebookLoginButtonRenderer : ViewRenderer<CustomControls.FacebookLoginButton, Xamarin.Facebook.Login.Widget.LoginButton>
    {
        Xamarin.Facebook.Login.Widget.LoginButton facebookLoginButton;

        /// <summary>
        /// Ons the element changed.
        /// </summary>
        /// <param name="e">E.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<CustomControls.FacebookLoginButton> e)
		{
			base.OnElementChanged(e);
			if (Control == null || facebookLoginButton == null)
			{
				facebookLoginButton = new Xamarin.Facebook.Login.Widget.LoginButton(Forms.Context);
				SetNativeControl(facebookLoginButton);
			}
		}
    }
}
