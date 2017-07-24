using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VadodaraByFoot.ServiceLayer;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;

namespace VadodaraByFoot.View.WalkTourModule
{
    public partial class WalkToursListPage : ContentPage
    {
        public List<Walktour> _walktourList =new List<Walktour>();
        public List<Walktour> FilteredWalkTourList = new List<Walktour>();
        public async void lstWalkTour_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
			var stack = Navigation.NavigationStack;
            if (stack[stack.Count - 1].GetType() != typeof(WalkTourDetailPage))
            {
                var objtour = e.SelectedItem as Walktour;
                if (AppStatics.DeviceHeightWithoutActionBar == 0)
                {
                    AppStatics.DeviceHeightWithoutActionBar = (int)this.Height;
                }

                await Navigation.PushAsync(new WalkTourDetailPage(objtour));
            }
        }
		
        public WalkToursListPage()
        {
            InitializeComponent();
			SetFontSizeControls();
        }
		public void SetFontSizeControls()
		{
            lblNoRecord.FontSize = AppStatics.GetFontSizeMedium();
            txtbxSearch.FontSize = AppStatics.GetFontSizeMedium();
		}
		protected override async void OnAppearing()
		{
			await CallWebserviceWalkTourList();
		}
		public async Task CallWebserviceWalkTourList()
		{
            if (AppStatics.CheckInternetConnection())
            {
                try
                {
                    RootObjectWalkTourList response = await CallWebservice.GetResponse_Get<RootObjectWalkTourList>(WebServiceURL.Url_walktourlst);
                    if (response != null)
                    {
                        if (response._resultflag == "1")
                        {
                            foreach (var tourobj in response.Walktour)
                                tourobj.TitleFontSize = AppStatics.GetFontSizeMedium();
                            lstWalkTour.ItemsSource = response.Walktour;
                            _walktourList = response.Walktour;
                            AppStatics.Loading(loading, false);
                            foreach (var objwalktour in response.Walktour)
                            {
                                objwalktour.title = objwalktour.title.ToUpper();
                                objwalktour.EndColorGradient =new Xamarin.Forms.Color(0, 0, 0, 0.8);
                                objwalktour.StartColorGradient = Xamarin.Forms.Color.Transparent;
                            }
                        }
                    }
                    else
                    {
                        AppStatics.Loading(loading, false);
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

		#region Toolbar item tapped events
		public void Notification_Clicked(object sender, System.EventArgs e)
		{
			DependencyService.Get<VadodaraByFoot.Interface.IAudioDownloadsList>().GetListOfFiles("");
        }

		public async void Search_Clicked(object sender, System.EventArgs e)
		{
            if (AppStatics.CheckInternetConnection())
            {
                if (TbSearch.Icon.File == "top_search_icon.png")
                {
                    SearchhStack.HeightRequest = 45;
                    SearchhStack.IsVisible = true;
                    TbSearch.Icon = "menu_drawer_close_icon.png";
                }
                else
                {
                    SearchhStack.HeightRequest = 0;
                    SearchhStack.IsVisible = false;
                    TbSearch.Icon = "top_search_icon.png";
                    lstWalkTour.ItemsSource = _walktourList;
                    txtbxSearch.Text = "";
                    lblNoRecord.IsVisible = false;
                    DependencyService.Get<VadodaraByFoot.Interface.IDissmissKeyboard>().DismissKeyboard();
                    //txtbxSearch.Unfocus();
                }
            }
			else
			{
				AppStatics.Loading(loading, false);
				await DisplayAlert(AppResources.AppResources.LError, AppResources.AppResources.LNoInternetConnection, AppResources.AppResources.LOk);
			}
		}

		public void txtbxSearch_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			FilteredWalkTourList = new List<Walktour>();

            if (_walktourList != null)
			{
                List<Walktour> newList = (from obj in _walktourList
                                          where  obj.title.ToLower().Contains(txtbxSearch.Text.ToLower())
                                          select obj).ToList<Walktour>();
				if (newList != null && newList.Any())
				{
                    FilteredWalkTourList = new List<Walktour>(newList);
				}
                lstWalkTour.ItemsSource = null;
                lstWalkTour.ItemsSource = FilteredWalkTourList;
                if (FilteredWalkTourList.Count == 0)
                    lblNoRecord.IsVisible = true;
                else
                    lblNoRecord.IsVisible = false;
             }
		}
		public void lstWalkTour_Refreshing(object sender, System.EventArgs e)
		{
            lstWalkTour.IsRefreshing = true;
            if (FilteredWalkTourList.Count== 0)
            {
                lstWalkTour.ItemsSource = null;
                lstWalkTour.ItemsSource = _walktourList;
                txtbxSearch.Text = "";
                lblNoRecord.IsVisible = false;
            }
            lstWalkTour.IsRefreshing = false;
        }
		#endregion

		#region Exit Application
		private bool _canClose = true;

		protected override bool OnBackButtonPressed()
		{
            if (_canClose)
			{
				ShowExitDialog();
			}
            return _canClose;
		}

		private async void ShowExitDialog()
		{
			var answer = await DisplayAlert(AppResources.AppResources.LExit, AppResources.AppResources.LExitApp, AppResources.AppResources.LYes, AppResources.AppResources.LNo);
			if (answer)
			{
				if (Device.OS == TargetPlatform.Android)
				{
                    DependencyService.Get<VadodaraByFoot.Interface.IExitApp>().CloseApp();
				}
				_canClose = false;
				OnBackButtonPressed();
			}
		}
		#endregion
	}
 }
