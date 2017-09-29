using SignUp.ViewModels;
using Xamarin.Forms;

namespace SignUp.Pages
{
    /// <summary>
    /// Show signups page.
    /// </summary>
    public partial class ShowSignupsPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Pages.ShowSignupsPage"/> class.
        /// </summary>
        public ShowSignupsPage()
        {
            InitializeComponent();

            BindingContext = new ShowSignupsPageViewModel();
        }
    }
}
