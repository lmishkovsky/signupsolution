using Xamarin.Forms;

namespace SignUp.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            // called from iOS once logged in Facebook
			App.PostSuccessFacebookAction = token =>
			{
                string message = string.Format("You are now logged in as: {0}", token);

                lblResult.Text =  message;

                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());
			};
        }
    }
}
