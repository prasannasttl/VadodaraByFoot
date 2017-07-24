using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VadodaraByFoot.ServiceLayer;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;
using VadodaraByFoot.CustomControls;
using System.Text.RegularExpressions;
using System.Text;

namespace VadodaraByFoot.View.LoginModule
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
			SetFontSizeControls();
		}
		public void SetFontSizeControls()
		{
            lblEmail.FontSize = AppStatics.GetFontSizeMedium();
            lblPassword.FontSize = AppStatics.GetFontSizeMedium();
            lblForgotPass.FontSize = AppStatics.GetFontSizeMedium();

			txtbxEmail.FontSize = AppStatics.GetFontSizeMedium();
            txtbxPassword.FontSize = AppStatics.GetFontSizeMedium();
            txtbxForgotPass.FontSize = AppStatics.GetFontSizeMedium();
			
            lblRegistration.FontSize = AppStatics.GetFontSizeSmall();
            lblDontHaveAccnt.FontSize = AppStatics.GetFontSizeSmall();
            LblForgotPassword.FontSize = AppStatics.GetFontSizeSmall();

            lblForgotPasTitlePage.FontSize = AppStatics.GetFontSizeTitle();

            btnLogin.FontSize = AppStatics.GetFontSizeMedium();
            btnForgotPass.FontSize = AppStatics.GetFontSizeMedium();
            btnForgotPassCancel.FontSize = AppStatics.GetFontSizeMedium();

            txtbxForgotPass.Placeholder = "Username or email address";
		}
        public async Task CallWebserviceLoginUser()
        {
            if (AppStatics.CheckInternetConnection())
            {
                try
                {
                    AppStatics.Loading(loading, true); 
                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("username", txtbxEmail.Text));

					#region  Password Encryption
					
                    byte[] PasswordBytes = Encoding.UTF8.GetBytes(txtbxPassword.Text);
					string base64StringPassword = Convert.ToBase64String(PasswordBytes);


					var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
					var stringChars = new char[8];
					var random = new Random();

					for (int i = 0; i < stringChars.Length; i++)
						stringChars[i] = chars[random.Next(chars.Length)];
					
					var finalString = new String(stringChars);
                    string EncryptPass = finalString + base64StringPassword + finalString;

                    byte[] EncryptedPasswordBytes = Encoding.UTF8.GetBytes(EncryptPass);
                    string SendEncryptedStringPass = Convert.ToBase64String(EncryptedPasswordBytes);
                    #endregion

                    values.Add(new KeyValuePair<string, string>("password", SendEncryptedStringPass));

                    var response = await VadodaraByFoot.ServiceLayer.CallWebservice.GetResponse_Post<RootObjectUserLogin>(WebServiceURL.Url_userLogin, values);
                    AppStatics.Loading(loading, false);
                    if (response != null)
                    {
                        if (response._resultflag == "1")
                        {
                            UserData _userdata = new UserData()
                            {
                                username = response.username,
                                name = response.name,
                                first_name = response.first_name,
                                last_name = response.last_name,
                                nickname = response.nickname,
                                slug = response.slug,
                                URL = response.URL,
                                avatar = response.avatar,
                                mobile = response.mobile,
                                description = response.description,
                                registered = response.registered,
                                email = response.email,
                                ID = response.ID
		                    };
                            AppStatics.SaveIsolatedData(_userdata);

							await Navigation.PopAsync();
                        }
                        else
                        {
                            await DisplayAlert(AppResources.AppResources.LMessage, response.message, AppResources.AppResources.LOk);
                        }
                    }
                    else
                    {
                        await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LWebserverNotResponding, AppResources.AppResources.LOk);
                    }
                }
                catch (Exception e)
                {
                    AppStatics.Loading(loading, false);
                    await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LSomethingWentWrong, AppResources.AppResources.LOk);
                }
            }
            else
            {
                AppStatics.Loading(loading, false);
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LNoInternetConnection, AppResources.AppResources.LOk);
            }
        }
        #region Entry text changed events
        public void Email_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbxEmail.Text))
                lblEmail.Opacity = 0;
            else
                lblEmail.Opacity = 1;
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);

        }

        public void Password_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbxPassword.Text))
                lblPassword.Opacity = 0;
            else
                lblPassword.Opacity = 1;
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
        }
        #endregion

        #region Tap events

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            btnLogin.IsEnabled = false;
            if (string.IsNullOrEmpty(txtbxEmail.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyEmailUsername, AppResources.AppResources.LOk);

			/* else if (!(Regex.Match(txtbxEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success))
				 XFToast.ShortMessage(AppResources.AppResources.LInValidEmail);*/
			else if (string.IsNullOrEmpty(txtbxPassword.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyPassword, AppResources.AppResources.LOk);

			else
                await CallWebserviceLoginUser();
			btnLogin.IsEnabled = true;
        }

        private async void OnRegisterTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(VadodaraByFoot.View.LoginModule.RegistrationPage))
            {
                await Navigation.PushAsync(new VadodaraByFoot.View.LoginModule.RegistrationPage());
            }
        }

        private async void OnForgotPassTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            txtbxForgotPass.Text = "";
            ForgotPassPopupStack.IsEnabled = true; ForgotPassPopupStack.IsVisible = true;
            LoginPageScrollView.IsEnabled = false;
            ForgotPassPopupStack.BackgroundColor = new Xamarin.Forms.Color(0, 0, 0, 0.5);
            LoginPageScrollView.Opacity = 0.05;
            LoginPageScrollView.BackgroundColor = new Xamarin.Forms.Color(0, 0, 0, 0.5);
        }

		#endregion

		#region Forgot PAss Popup methods

		public async System.Threading.Tasks.Task CallWebserviceForgotPassUser()
		{
			if (AppStatics.CheckInternetConnection())
			{
				try
				{
                    AppStatics.Loading(loading, true); 
					List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
					values.Add(new KeyValuePair<string, string>("user_login", txtbxForgotPass.Text));

					var response = await VadodaraByFoot.ServiceLayer.CallWebservice.GetResponse_Post<RootObjectUserForgotPass>(WebServiceURL.Url_userForgotPass, values);
					AppStatics.Loading(loading, false);
					if (response != null)
					{

						await DisplayAlert(AppResources.AppResources.LMessage, response.message, AppResources.AppResources.LOk);
					}
					else
					{
						await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LWebserverNotResponding, AppResources.AppResources.LOk);
					}
				}
				catch (Exception e)
				{
					AppStatics.Loading(loading, false);
					await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LSomethingWentWrong, AppResources.AppResources.LOk);
				}
			}
			else
			{
				AppStatics.Loading(loading, false);
				await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LNoInternetConnection, AppResources.AppResources.LOk);
			}

            ForgotPassPopupStack.IsEnabled = false; ForgotPassPopupStack.IsVisible = false;
            LoginPageScrollView.Opacity = 1;
            LoginPageScrollView.IsEnabled = true;
            LoginPageScrollView.BackgroundColor = Color.Transparent;
		}
		#region Entry text changed events
		public void ForgotPass_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
		/*	if (String.IsNullOrEmpty(txtbxForgotPass.Text))
				lblForgotPass.Opacity = 0;
			else
				lblForgotPass.Opacity = 1;*/
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
		}
		#endregion

		#region Tap events

		private async void ForgotPassButton_Clicked(object sender, EventArgs e)
		{
            btnForgotPass.IsEnabled = false;
			if (string.IsNullOrEmpty(txtbxForgotPass.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyEmailUsername, AppResources.AppResources.LOk);

			else
				await CallWebserviceForgotPassUser();
			btnForgotPass.IsEnabled = true;
		}
        private void ForgotPassCancelButton_Clicked(object sender, EventArgs e)
        {
			ForgotPassPopupStack.IsEnabled = false; ForgotPassPopupStack.IsVisible = false;
			LoginPageScrollView.Opacity = 1;
            LoginPageScrollView.BackgroundColor = Color.Transparent;
            LoginPageScrollView.IsEnabled = true;
        }
		#endregion
		#endregion
	}
}
