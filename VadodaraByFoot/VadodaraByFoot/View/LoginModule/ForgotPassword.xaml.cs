using System;
using System.Collections.Generic;
using System.Text;
using VadodaraByFoot.CustomControls;
using VadodaraByFoot.ServiceLayer;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;

namespace VadodaraByFoot.View.LoginModule
{
    public partial class ForgotPassword : ContentPage
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

		public async System.Threading.Tasks.Task CallWebserviceForgotPassUser()
		{
			if (AppStatics.CheckInternetConnection())
			{
				try
				{
					AppStatics.Loading(loading, true);
					List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("username", txtbxForgotPass.Text));

                    var response = await VadodaraByFoot.ServiceLayer.CallWebservice.GetResponse_Post<RootObjectUserForgotPass>(WebServiceURL.Url_userForgotPass, values);
					AppStatics.Loading(loading, false);
					if (response != null)
					{
						
							await DisplayAlert(AppResources.AppResources.LMessage, response.message, AppResources.AppResources.LOk);
							await Navigation.PopAsync();
						
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
		public void ForgotPass_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
            if (String.IsNullOrEmpty(txtbxForgotPass.Text))
                lblForgotPass.Opacity = 0;
			else
				lblForgotPass.Opacity = 1;
        }
        #endregion

		#region Tap events

		private async void ForgotPassButton_Clicked(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtbxForgotPass.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyEmailUsername, AppResources.AppResources.LOk);

			else
				await CallWebserviceForgotPassUser();
		}

		#endregion
	}
}
