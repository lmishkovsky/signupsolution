using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Settings;
using SignUp.FacebookHelper;
using Xamarin.Forms;

namespace SignUp.Pages
{
    /// <summary>
    /// Login dependency page.
    /// </summary>
    public class LoginDependencyPage : ContentPage
    {
        const string TITLE = "Account";
        const string TELLUSWHOYOUARE = "Tell us who you are.";
        const string NEXT = "NEXT";

        Image _imageProfile;
        Button _btnNext;

		readonly Label _hintLabel;
		readonly List<Button> _loginButtons = new List<Button>();
		bool _isAuthenticated;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SignUp.Pages.LoginDependencyPage"/> class.
        /// </summary>
		public LoginDependencyPage()
		{
            // the page title
            Title = TITLE;

            Icon = "ic_account_box.png";

            // the facebook image
            _imageProfile = new Image();

            // the next button which brings us to the next page
            _btnNext = new Button();
            _btnNext.Text = NEXT;
            _btnNext.BackgroundColor = Color.Teal;
            _btnNext.TextColor = Color.White;
            _btnNext.WidthRequest = 120;
            _btnNext.Margin = new Thickness(120, 40, 120, 0);
            _btnNext.IsVisible = false;
            _btnNext.Clicked += NextButtonClicked;

			_hintLabel = new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				Text = TELLUSWHOYOUARE
			};

			var stackLayout = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
                Children = { _hintLabel, _imageProfile }
			};

			// var providers = new[] { "Facebook", "VK", "Microsoft" };

            var providers = new[] { "Facebook" };

			foreach (var provider in providers)
			{
				var loginButton = new Button
				{
					HorizontalOptions = LayoutOptions.Center,
					Text = $"Login {provider}",
					AutomationId = provider
				};

				loginButton.Clicked += LoginButtonOnClicked;

				_loginButtons.Add(loginButton);
				stackLayout.Children.Add(loginButton);
			}

            stackLayout.Children.Add(_btnNext);

			Content = stackLayout;

            // GoToGroupPageIfAlreadyLoggedIn();
		}

        /// <summary>
        /// Gos to group page if not first login.
        /// </summary>
        private void GoToGroupPageIfAlreadyLoggedIn()
        {
            var imageUrl = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookImage, string.Empty);

            if (!String.IsNullOrEmpty(imageUrl))
            {
                var userName = CrossSettings.Current.GetValueOrDefault(Constants.CrossSettingsKeys.FacebookName, string.Empty);

                _hintLabel.Text = $"Hi {userName}!";
				_loginButtons[0].Text = $"Logout Facebook";
                _imageProfile.Source = imageUrl;
				_imageProfile.WidthRequest = 200;
				_imageProfile.HeightRequest = 200;

                _isAuthenticated = true;
                _btnNext.IsVisible = true;

                Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());
            }
        }

        /// <summary>
        /// Next button clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void NextButtonClicked(object sender, EventArgs e)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());
        }

        /// <summary>
        /// Logins the button on clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
		async void LoginButtonOnClicked(object sender, EventArgs e)
		{
            try
            {
                if (_isAuthenticated)
                {
                    _hintLabel.Text = TELLUSWHOYOUARE;
                    _imageProfile.Source = null;
					_imageProfile.WidthRequest = 0;
					_imageProfile.HeightRequest = 0;

                    // save settings
                    CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookID, string.Empty);
                    CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookName, string.Empty);
                    CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookEmail, string.Empty);
                    CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookImage, string.Empty);

                    // hide next button
                    _btnNext.IsVisible = false;

                    var senderBtn = sender as Button;
                    if (senderBtn == null) return;

                    Logout(senderBtn.AutomationId);

                    _isAuthenticated = false;
                    foreach (var btn in _loginButtons)
                    {
                        btn.IsEnabled = true;
                        btn.Text = $"Login {btn.AutomationId}";
                    }
                }
                else
                {
                    var senderBtn = sender as Button;
                    if (senderBtn == null) return;

                    _hintLabel.Text = "Login. Please wait";
                    var loginResult = await Login(senderBtn.AutomationId);

                    foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                        btn.IsEnabled = false;

                    switch (loginResult.LoginState)
                    {
                        case LoginState.Canceled:
                            _hintLabel.Text = "Canceled";
                            foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                                btn.IsEnabled = true;
                            break;
                        case LoginState.Success:
                            _hintLabel.Text = $"Hi {loginResult.FirstName}!";
                            senderBtn.Text = $"Logout {senderBtn.AutomationId}";
							_imageProfile.Source = loginResult.ImageUrl;
							_imageProfile.WidthRequest = 200;
							_imageProfile.HeightRequest = 200;

                            // save settings
                            CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookID, loginResult.UserId);
                            CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookName, loginResult.FirstName + " " + loginResult.LastName);
                            CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookEmail, loginResult.Email);
                            CrossSettings.Current.AddOrUpdateValue(SignUp.Constants.CrossSettingsKeys.FacebookImage, loginResult.ImageUrl);

                            _btnNext.IsVisible = true;

                            _isAuthenticated = true;

                            //await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new SignUp.Pages.GroupCodePage());

                            break;
                        default:
                            _hintLabel.Text = "Failed: " + loginResult.ErrorString;
                            foreach (var btn in _loginButtons.Where(b => b != senderBtn))
                                btn.IsEnabled = true;
                            break;
                    }
                }
            }
            catch (Exception ex) {
                await Application.Current.MainPage.DisplayAlert("Alert", ex.ToString(), "OK");
            }
		}

		Task<LoginResult> Login(string providerName)
		{
			switch (providerName.ToLower())
			{
				case "vk":
					//return DependencyService.Get<IVkService>().Login();
				case "facebook":
					return DependencyService.Get<IFacebookService>().Login();
				default:
					//return DependencyService.Get<IOAuthService>().Login();
                    return DependencyService.Get<IFacebookService>().Login();
			}
		}

		void Logout(string providerName)
		{
			switch (providerName.ToLower())
			{
				case "vk":
					//DependencyService.Get<IVkService>().Logout();
					return;
				case "facebook":
					DependencyService.Get<IFacebookService>().Logout();
					return;
				default:
					//DependencyService.Get<IOAuthService>().Logout();
					return;
			}
		}
    }
}

