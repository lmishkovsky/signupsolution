using Foundation;
using UIKit;

namespace SignUp.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// Finished the launching.
        /// </summary>
        /// <returns><c>true</c>, if launching was finisheded, <c>false</c> otherwise.</returns>
        /// <param name="uiApplication">User interface application.</param>
        /// <param name="launchOptions">Launch options.</param>
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Facebook.CoreKit.Settings.AppID = "351149828643713";
            Facebook.CoreKit.Settings.DisplayName = "SignUp";

            global::Xamarin.Forms.Forms.Init();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            LoadApplication(new App());

            Facebook.CoreKit.Profile.EnableUpdatesOnAccessTokenChange(true);
            Facebook.CoreKit.ApplicationDelegate.SharedInstance.FinishedLaunching(uiApplication, launchOptions);
            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        /// <summary>
        /// Opens the URL.
        /// </summary>
        /// <returns><c>true</c>, if URL was opened, <c>false</c> otherwise.</returns>
        /// <param name="application">Application.</param>
        /// <param name="url">URL.</param>
        /// <param name="sourceApplication">Source application.</param>
        /// <param name="annotation">Annotation.</param>
		public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
            return Facebook.CoreKit.ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
		}

        /// <summary>
        /// Ons the activated.
        /// </summary>
        /// <param name="uiApplication">User interface application.</param>
		public override void OnActivated(UIApplication uiApplication)
		{
			base.OnActivated(uiApplication);
			Facebook.CoreKit.AppEvents.ActivateApp();
		}
    }
}
