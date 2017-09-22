using Xamarin.Forms;

namespace SignUp.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

			App.PostSuccessFacebookAction = token =>
			{
                string message = string.Format("You are now logged in as: {0}", token);

                lblResult.Text =  message;
			};
        }
    }
}
