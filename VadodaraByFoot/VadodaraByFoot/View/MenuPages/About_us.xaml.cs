using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VadodaraByFoot.CustomControls;
using VadodaraByFoot.ServiceLayer;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;

namespace VadodaraByFoot.View.MenuPages
{
    public partial class About_us : ContentPage
    {
        RootObjectAboutUs DetailData = new RootObjectAboutUs();
        public About_us()
        {
            InitializeComponent();
            designchanges();

        }

        public void designchanges()
        {
            imgHeader.HeightRequest = AppStatics.DeviceHeight * 0.40;
            imgHeaderPlaceholder.HeightRequest = AppStatics.DeviceHeight * 0.40;
            imgHeaderGrid.HeightRequest = AppStatics.DeviceHeight * 0.40;
            //imgHeaderGradientGrid.HeightRequest = AppStatics.DeviceHeight * 0.30;
            if ((Device.Idiom == TargetIdiom.Tablet) && (Device.OS == TargetPlatform.iOS))
            {
                imgHeader.HeightRequest = 420;
                imgHeaderGrid.HeightRequest = 420;
                imgHeaderPlaceholder.HeightRequest = 420;
                //imgHeaderGradientGrid.HeightRequest = 420;
            }
			SetFontSizeControls();
        }
		public void SetFontSizeControls()
		{
            lblTourName.FontSize = VadodaraByFoot.AppStatics.GetFontSizeTitle();
			lblTourDescription.FontSize = VadodaraByFoot.AppStatics.GetFontSizeSmall();
		}
        protected override async void OnAppearing()
        {
            await CallWebserviceAboutUs();
        }
        public async Task CallWebserviceAboutUs()
        {
            if (AppStatics.CheckInternetConnection())
            {
                try
                {
                    RootObjectAboutUs response = await CallWebservice.GetResponse_Get<RootObjectAboutUs>(WebServiceURL.Url_AboutUs);

                    if (response != null)
                    {
                        if (response._resultflag == "1")
                        {
                            DetailData = response;
                            imgHeader.Source = response.is_image;
                            lblTourDescription.Text = response.content;
                            lblTourName.Text = AppResources.AppResources.LAbt_Vadodara_By_Foot;
                            ContentLayout.IsVisible = true;
                        }
                    }
                    else
                    {
                        await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LWebserverNotResponding, AppResources.AppResources.LOk);
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LSomethingWentWrong, AppResources.AppResources.LOk);
                }
            }
            else
            {
                await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LNoInternetConnection, AppResources.AppResources.LOk);
            }
            AppStatics.Loading(loading, false);
        }
        #region Toolbar item tapped events
        public void Share_Clicked(object sender, System.EventArgs e)
        {

        }
		#endregion

		protected override bool OnBackButtonPressed()
		{
            Application.Current.MainPage = new VadodaraByFoot.View.MenuModule.MenuMaster();
			return true;
		}
    }
}
