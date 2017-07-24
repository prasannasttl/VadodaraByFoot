using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VadodaraByFoot.ServiceLayer;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;
using VadodaraByFoot.AppResources;
using VadodaraByFoot.CustomControls;
using System.Text;

namespace VadodaraByFoot.View.LoginModule
{
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();
			SetFontSizeControls();
		}
		public void SetFontSizeControls()
		{
            lblName.FontSize = AppStatics.GetFontSizeMedium();
            lblEmail.FontSize = AppStatics.GetFontSizeMedium();
            lblMobNum.FontSize = AppStatics.GetFontSizeMedium();
            lblPass.FontSize = AppStatics.GetFontSizeMedium();
            lblConfirmPass.FontSize = AppStatics.GetFontSizeMedium();
            lblFirstName.FontSize =  AppStatics.GetFontSizeMedium();
            lblLastName.FontSize = AppStatics.GetFontSizeMedium();

            txtbxName.FontSize = AppStatics.GetFontSizeMedium();
            txtbxEmail.FontSize = AppStatics.GetFontSizeMedium();
            txtbxMobNum.FontSize = AppStatics.GetFontSizeMedium();
            txtbxPass.FontSize = AppStatics.GetFontSizeMedium();
            txtbxConfirmPass.FontSize = AppStatics.GetFontSizeMedium();
            txtbxFirstName.FontSize = AppStatics.GetFontSizeMedium();
            txtbxLastName.FontSize = AppStatics.GetFontSizeMedium();

            lblLogin.FontSize = AppStatics.GetFontSizeSmall();
            lblAlreadyHaveAccnt.FontSize = AppStatics.GetFontSizeSmall();

            lblAgreeTermCondition.FontSize = AppStatics.GetFontSizeSmall();
            lblAgreeTermConditionLink.FontSize = AppStatics.GetFontSizeSmall();

            btnRegistration.FontSize = AppStatics.GetFontSizeMedium();
        }

		public async Task CallWebserviceRegisterUser()
		{
            if (AppStatics.CheckInternetConnection())
            {
                try
                {
                    AppStatics.Loading(loading, true);
                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("first_name", txtbxFirstName.Text));
                    values.Add(new KeyValuePair<string, string>("last_name", txtbxLastName.Text));
                    values.Add(new KeyValuePair<string, string>("username", txtbxName.Text));

					#region  Password Encryption

                    byte[] PasswordBytes = Encoding.UTF8.GetBytes(txtbxPass.Text);
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
					
                    values.Add(new KeyValuePair<string, string>("confirmpassword", SendEncryptedStringPass));


					//values.Add(new KeyValuePair<string, string>("password", txtbxPass.Text));
					values.Add(new KeyValuePair<string, string>("email", txtbxEmail.Text));
                    values.Add(new KeyValuePair<string, string>("mobile", txtbxMobNum.Text));
                    var response = await CallWebservice.GetResponse_Post<RootObjectUserRegistration>(WebServiceURL.Url_userReg, values);
                    AppStatics.Loading(loading, false);
                    if (response != null)
                    {
                        await DisplayAlert(AppResources.AppResources.LMessage, response.message, AppResources.AppResources.LOk);
                        if (response._resultflag == 1)
                            Navigation.PopAsync();
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
		public void FirstName_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
            if (String.IsNullOrEmpty(txtbxFirstName.Text))
                lblFirstName.Opacity = 0;
			else
				lblFirstName.Opacity = 1;
			VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
		}
		public void LastName_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
            if (String.IsNullOrEmpty(txtbxLastName.Text))
                lblLastName.Opacity = 0;
			else
                lblLastName.Opacity = 1;
			VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
		}
        public void Name_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbxName.Text))
                lblName.Opacity = 0;
            else
                lblName.Opacity = 1;
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
        }

        public void Email_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbxEmail.Text))
                lblEmail.Opacity = 0;
            else
                lblEmail.Opacity = 1;
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
        }

        public void MobNum_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbxMobNum.Text))
                lblMobNum.Opacity = 0;
            else
                lblMobNum.Opacity = 1;
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
        }

        public void Password_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbxPass.Text))
                lblPass.Opacity = 0;
            else
                lblPass.Opacity = 1;
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
        }

        public void ConfirmPassword_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtbxConfirmPass.Text))
                lblConfirmPass.Opacity = 0;
            else
                lblConfirmPass.Opacity = 1;
            VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
        }

		#endregion
		#region Tap Events

		private async void RegistrationButton_Clicked(object sender, EventArgs e)
		{
            btnRegistration.IsEnabled = false;
            if (string.IsNullOrEmpty(txtbxFirstName.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyFirstName, AppResources.AppResources.LOk);

			else if (string.IsNullOrEmpty(txtbxLastName.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyLastName, AppResources.AppResources.LOk);

			else if (string.IsNullOrEmpty(txtbxName.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyFullName, AppResources.AppResources.LOk);

			else if (string.IsNullOrEmpty(txtbxEmail.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyEmail, AppResources.AppResources.LOk);

			else if (!(Regex.Match(txtbxEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LInValidEmail, AppResources.AppResources.LOk);

			else if (string.IsNullOrEmpty(txtbxPass.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyPassword, AppResources.AppResources.LOk);

			else if ((txtbxConfirmPass.Text != txtbxPass.Text) && (!string.IsNullOrEmpty(txtbxPass.Text)) && (!string.IsNullOrEmpty(txtbxConfirmPass.Text)))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LPassNotMatch, AppResources.AppResources.LOk);

			else if (string.IsNullOrEmpty(txtbxMobNum.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyMobileNo, AppResources.AppResources.LOk);

			else if (chckbxTermsConditions.Checked == false)
				await DisplayAlert(AppResources.AppResources.LError, "Please Agree on terms and conditions", AppResources.AppResources.LOk);
            
			else
				await CallWebserviceRegisterUser();
			btnRegistration.IsEnabled = true;
		}
		private void OnLoginTapGestureRecognizerTapped(object sender, EventArgs e)
		{
            Navigation.PopAsync();
		}
        private void OnTermsConditionTapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.google.co.in/"));
        }
		#endregion

	}
}
