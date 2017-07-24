using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VadodaraByFoot.CustomControls;
using VadodaraByFoot.ServiceLayer;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;

namespace VadodaraByFoot.View.WalkTourModule
{
    public partial class WalkTourDetailPage : ContentPage
    {
        public string _tourId = "";
        RootObjectWalkTourDetail DetailData = new RootObjectWalkTourDetail();
        public WalkTourDetailPage(Walktour walktour)
        {
            InitializeComponent();
            imgHeader.Source = walktour.is_image;
            _tourId = walktour.ID;
            designchanges();
        }
		public void SetFontSizeControls()
		{
            lblTourName.FontSize = VadodaraByFoot.AppStatics.GetFontSizeTitle();
			lblTourDescription.FontSize = VadodaraByFoot.AppStatics.GetFontSizeSmall();
		}
        public void designchanges()
        {
            imgHeader.HeightRequest = AppStatics.DeviceHeight * 0.40;
            imgHeaderGrid.HeightRequest = AppStatics.DeviceHeight * 0.40;
            imgHeaderGradientGrid.HeightRequest = AppStatics.DeviceHeight * 0.40;
            imgHeaderPlaceholder.HeightRequest = AppStatics.DeviceHeight * 0.40;
            if ((Device.Idiom == TargetIdiom.Tablet) && (Device.OS == TargetPlatform.iOS))
            {
                imgHeader.HeightRequest = 420;
                imgHeaderGrid.HeightRequest = 420;
                imgHeaderGradientGrid.HeightRequest = 420;
                imgHeaderPlaceholder.HeightRequest = 420;
            }
            SetFontSizeControls();
        }

        protected override async void OnAppearing()
        {
            await CallWebserviceWalkTourDetail();
        }
        protected override void OnDisappearing()
        {
			AppStatics.Loading(loading, false);
        }
        public async Task CallWebserviceWalkTourDetail()
        {
            if (AppStatics.CheckInternetConnection())
            {
                try
                {
                    RootObjectWalkTourDetail response = await CallWebservice.GetResponse_Get<RootObjectWalkTourDetail>(WebServiceURL.Url_walktourdetail + _tourId);

                    if (response != null)
                    {
                        if (response._resultflag == "1")
                        {
                            DetailData = response;
                            lblTourDescription.Text = response.Walktourdetail.excerpt ;
                            lblTourName.Text = response.Walktourdetail.title.ToUpper();

                            #region 
                            await CallMapDataPoints();
                            #endregion
                         /*   AppStatics.Loading(loading, false);
                            ContentLayout.IsVisible = true;
                            imgPlaceDetail.IsVisible = true;*/
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
        public async void Back_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
        public void Share_Clicked(object sender, System.EventArgs e)
        {

        }
        public async void PlaceDetail_Clicked(object sender, System.EventArgs e)
        {
			AppStatics.Loading(loading, true);
			//   await Navigation.PushAsync(new VadodaraByFoot.View.WalkTourModule.RouteCoversPage(DetailData, MapDataObject , imgHeader.Source));
			UserData _userdata = AppStatics.LoadIsolatedData();

			if (_userdata != null)
                 {
					var stack = Navigation.NavigationStack;
                    if (stack[stack.Count - 1].GetType() != typeof(RouteCoversPage))
                    { await Navigation.PushAsync(new VadodaraByFoot.View.WalkTourModule.RouteCoversPage(DetailData, MapDataObject, imgHeader.Source)); }
                 }
                 else
                 {
					var stack = Navigation.NavigationStack;
                    if (stack[stack.Count - 1].GetType() != typeof(VadodaraByFoot.View.LoginModule.LoginPage))
                    { await Navigation.PushAsync(new VadodaraByFoot.View.LoginModule.LoginPage()); }
                 }
           }

        #endregion

        #region Map service calling
        VadodaraByFoot.ServiceLayer.ServiceClass.MapResponse.RootObject MapDataObject = new VadodaraByFoot.ServiceLayer.ServiceClass.MapResponse.RootObject();
		public String getDirectionsUrl()
		{
			var TourList = DetailData.Walktourdetail.meta.tour_location;
			var mapApiKey = "key=AIzaSyDQjEKlHthtp6SLTgjWHAPrbX0NZN1cpRA";
		
			//  var url = "https://maps.googleapis.com/maps/api/directions/json?origin=Boston,MA&destination=Concord,MA&waypoints=Charlestown,MA|via:Lexington,MA&key=AIzaSyDQjEKlHthtp6SLTgjWHAPrbX0NZN1cpRA";
			//var url = "https://maps.googleapis.com/maps/api/directions/json?origin=SilverTouchTechnologiesLimited&destination=IIM&key=AIzaSyDQjEKlHthtp6SLTgjWHAPrbX0NZN1cpRA";

			// Origin of route
            String str_origin = "origin=" + TourList[0].location_latitude + "," +TourList[0].location_longitude;

			// Destination of route
            String str_dest = "destination=" + TourList[TourList.Count - 1].location_latitude + "," + TourList[TourList.Count - 1].location_longitude;

			// Sensor enabled
			String sensor = "sensor=false";

			// Waypoints
			String waypoints = "";
            int polypoint;
            if (TourList.Count > 10)
                polypoint = 8;
            else
                polypoint = TourList.Count-2;
            
            for (int i = 0; i < polypoint; i++)
			{
				if (i == 0)
					waypoints = "waypoints=";
                waypoints =waypoints+ TourList[i+1].location_latitude + "," +TourList[i+1].location_longitude ;
                if (i != (polypoint-1))
                    waypoints = waypoints+"|";
			}

			// Building the parameters to the web service
			String parameters = str_origin + "&" + str_dest + "&"  + waypoints;

			// Output format
			String output = "json";

			// Building the url to the web service
            String url = "https://maps.googleapis.com/maps/api/directions/" + output + "?" + parameters+"&"+ sensor + "&"+mapApiKey;

			return url;
		}

        protected async Task CallMapDataPoints()
		{
            string url= getDirectionsUrl();
          
			var response = await VadodaraByFoot.ServiceLayer.ServiceClass.GetResponseFromWebService.GetResponse<VadodaraByFoot.ServiceLayer.ServiceClass.MapResponse.RootObject>(url);


			if (response != null)
			{

				//List<LatiLong> res = new List<LatiLong>();
				foreach (var Item in response.routes)
				{
					foreach (var Item1 in Item.legs)
					{
						foreach (var Steps in Item1.steps)
						{
							CustomMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(Steps.start_location.lat, Steps.start_location.lng));

							var res = DecodePolylinePoints(Steps.polyline.points);
							foreach (var Values in res)
							{
								CustomMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(Values.lat, Values.lng));
							}
							CustomMap.RouteCoordinates.Add(new Xamarin.Forms.Maps.Position(Steps.end_location.lat, Steps.end_location.lng));
						}
					}
				}
			}
            MapDataObject =  response;
			//await this.Navigation.PushAsync(new MapPage(response), true);
		}

		private List<MapResponse.LatiLong> DecodePolylinePoints(string encodedPoints)
		{
			if (encodedPoints == null || encodedPoints == "") return null;
			List<MapResponse.LatiLong> poly = new List<MapResponse.LatiLong>();
			char[] polylinechars = encodedPoints.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;
			int next5bits;
			int sum;
			int shifter;

			try
			{
				while (index < polylinechars.Length)
				{
					// calculate next latitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length)
						break;

					currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

					//calculate next longitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length && next5bits >= 32)
						break;

					currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
					MapResponse.LatiLong p = new MapResponse.LatiLong();
					p.lat = Convert.ToDouble(currentLat) / 100000.0;
					p.lng = Convert.ToDouble(currentLng) / 100000.0;
					poly.Add(p);
				}
			}
			catch (Exception ex)
			{
				// logo it
			}
			AppStatics.Loading(loading, false);
			ContentLayout.IsVisible = true;
			imgPlaceDetail.IsVisible = true;
           	return poly;
		}

        #endregion
    }
}
