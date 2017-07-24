using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VadodaraByFoot.CustomControls;
using VadodaraByFoot.ServiceLayer;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;

namespace VadodaraByFoot.View.MenuPages
{
    public partial class FeedBack : ContentPage
    {
        public FeedBack()
        {
            InitializeComponent();
			SetFontSizeControls();
		}
		public void SetFontSizeControls()
		{
			lblFullName.FontSize = AppStatics.GetFontSizeMedium();
			lblEmail.FontSize = AppStatics.GetFontSizeMedium();
			lblSubject.FontSize = AppStatics.GetFontSizeMedium();
			lblMessage.FontSize = AppStatics.GetFontSizeMedium();

			txtbxFullName.FontSize = AppStatics.GetFontSizeMedium();
			txtbxEmail.FontSize = AppStatics.GetFontSizeMedium();
			txtbxSubject.FontSize = AppStatics.GetFontSizeMedium();
			txtbxMessage.FontSize = AppStatics.GetFontSizeMedium();
			MessageTextOverlap.FontSize = AppStatics.GetFontSizeMedium();

            btnSubmit.FontSize = AppStatics.GetFontSizeMedium();
            lblFeedBackDescription.FontSize = AppStatics.GetFontSizeSmall();
		}
		public void FullName_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
            if (String.IsNullOrEmpty(txtbxFullName.Text))
                lblFullName.Opacity = 0;
			else
				lblFullName.Opacity = 1;
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

		public void Subject_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
            if (String.IsNullOrEmpty(txtbxSubject.Text))
                lblSubject.Opacity = 0;
			else
				lblSubject.Opacity = 1;
			VadodaraByFoot.AppStatics.CheckMaxLengthEntry(sender);
		}

		public void Message_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			if (String.IsNullOrEmpty(txtbxMessage.Text))
				lblMessage.Opacity = 0;
			else
				lblMessage.Opacity = 1;

			Editor entry = sender as Editor;
			string EntryText = entry.Text;
			string Maxlength = entry.ClassId;
			if (entry.Text == "")
				MessageTextOverlap.IsVisible = true;
			else
				MessageTextOverlap.IsVisible = false;


			if (EntryText.Length > Int32.Parse(Maxlength))//If it is more than your character restriction
			{
				EntryText = EntryText.Remove(EntryText.Length - 1);// Remove Last character 
				txtbxMessage.Text = EntryText; //Set the Old value
			}
		}

		private async void SubmitButton_Clicked(object sender, EventArgs e)
		{
			btnSubmit.IsEnabled = false;
            if (string.IsNullOrEmpty(txtbxFullName.Text))
				await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyFullName, AppResources.AppResources.LOk);

			
			else if (string.IsNullOrEmpty(txtbxEmail.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyEmail, AppResources.AppResources.LOk);

			else if (!(Regex.Match(txtbxEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LInValidEmail, AppResources.AppResources.LOk);

			else if (string.IsNullOrEmpty(txtbxSubject.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptySubject, AppResources.AppResources.LOk);

			else if (string.IsNullOrEmpty(txtbxMessage.Text))
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LEmptyMessage, AppResources.AppResources.LOk);

			else
				await CallWebserviceFeedback();
			btnSubmit.IsEnabled = true;
		}

		public async System.Threading.Tasks.Task CallWebserviceFeedback()
		{
			if (AppStatics.CheckInternetConnection())
			{
				try
				{
					AppStatics.Loading(loading, true);
					List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("your-name", txtbxFullName.Text));
                    values.Add(new KeyValuePair<string, string>("your-email", txtbxEmail.Text));
                    values.Add(new KeyValuePair<string, string>("your-subject", txtbxSubject.Text));
                    values.Add(new KeyValuePair<string, string>("your-message", txtbxMessage.Text));
                    values.Add(new KeyValuePair<string, string>("mobile-no", "9898898989"));
                    var response = await CallWebservice.GetResponse_Post<RootObjectFeedback>(WebServiceURL.Url_Feedback, values);
					AppStatics.Loading(loading, false);
					if (response != null)
					{
						await DisplayAlert(AppResources.AppResources.LMessage, response.message, AppResources.AppResources.LOk);
                        if (response._resultflag == "1")
                        {
                            txtbxFullName.Text = "";
                            txtbxEmail.Text = "";
                            txtbxSubject.Text = "";
                            txtbxMessage.Text = "";
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
		protected override bool OnBackButtonPressed()
		{
			Application.Current.MainPage = new VadodaraByFoot.View.MenuModule.MenuMaster();
			return true;
		}
    }
}
