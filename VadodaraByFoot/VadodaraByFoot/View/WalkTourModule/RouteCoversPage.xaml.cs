using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VadodaraByFoot.Interface;

using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace VadodaraByFoot.View.WalkTourModule
{
    public partial class RouteCoversPage : ContentPage
    {
        #region variables listbinding
        List<TourLocation> _routeCovers = new List<TourLocation>();
        RootObjectWalkTourDetail _rootobjdetailData = new RootObjectWalkTourDetail();
        ImageSource _tourPlaceURl;
        public int SelectTourIndex=0;
        VadodaraByFoot.ServiceLayer.ServiceClass.MapResponse.RootObject _responseMap;
		public IAdvancedTimer timer;
        public IAdvancedTimer BufferTimer;
        TourLocation SelectedTour = new TourLocation();
		#endregion

		#region Constructor and design
		public RouteCoversPage(RootObjectWalkTourDetail rootobjdetailData, VadodaraByFoot.ServiceLayer.ServiceClass.MapResponse.RootObject responseMap, ImageSource tourPlaceUrl)
        {
            InitializeComponent();

            _tourPlaceURl = tourPlaceUrl;
            _rootobjdetailData = rootobjdetailData;
            _responseMap = responseMap;
            _routeCovers = _rootobjdetailData.Walktourdetail.meta.tour_location;
           
            if(Device.OS == TargetPlatform.iOS)
				MapBindpinsInMap(_responseMap);
        }
        protected override void OnAppearing()
        {
            imgHeader.Source = _tourPlaceURl;
            routeCoversListBindData(0);

            UpdateScreenData(_routeCovers[0]);
            SelectedTour = _routeCovers[0];
			timer = DependencyService.Get<IAdvancedTimer>();
           
            timer.InitTimer(3000, timerElapsed, false);
            timer.StartTimer();
         
            if(Device.OS == TargetPlatform.Android)
				MapBindpinsInMap(_responseMap);
		
        }
        public void designchanges()
        {
            imgHeader.HeightRequest = AppStatics.DeviceHeight * 0.30;
            imgHeaderPlaceholder.HeightRequest = AppStatics.DeviceHeight * 0.30;
            if ((Device.Idiom == TargetIdiom.Tablet) && (Device.OS == TargetPlatform.iOS))
            {
                imgHeader.HeightRequest = 420;
                imgHeaderPlaceholder.HeightRequest = 420;
            }
            AudioPlayerBufferingPanel.HeightRequest = AudioPlayerMainPanel.HeightRequest;
            SetFontSizeControls();
        }

		public void SetFontSizeControls()
        {
            lblDownload.FontSize = VadodaraByFoot.AppStatics.GetFontSizeSmall();
            lblTourName.FontSize = VadodaraByFoot.AppStatics.GetFontSizeTitle();
            lblAudioDuration.FontSize = VadodaraByFoot.AppStatics.GetFontSizeSmall();
            lblDownloadingSts.FontSize = VadodaraByFoot.AppStatics.GetFontSizeSmall();
            lblTourDescription.FontSize = VadodaraByFoot.AppStatics.GetFontSizeSmall();
        }
        #endregion

        #region Place Number List Methods
        public void routeCoversListBindData(int PlaceSelectedIndex)
        {
            int plcenumber = 1;
            for (int i = 0; i < _routeCovers.Count; i++)
            {
                _routeCovers[i].placeNumberRing = "route_scroller_nor.png";
                _routeCovers[i].placeNumberTitle = plcenumber.ToString();
                _routeCovers[i].SeparatorWidth = 35;
                _routeCovers[i].NumberFontSize = AppStatics.GetFontSizeMedium();
                _routeCovers[i].SpacingBetweenNumber = 0;
                plcenumber++;
            }
            _routeCovers[PlaceSelectedIndex].placeNumberRing = "route_scroller_sel.png";
            _routeCovers[_routeCovers.Count - 1].SeparatorWidth = 0;
          //  lstPlaceNumber.ItemsSource = null;
            lstPlaceNumber.ItemsSource = _routeCovers;

			lstPlaceNumber.Render();
            designchanges();
        }
		public void timerElapsed(object o, EventArgs e)
		{
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			{
                
				LoadAudioFile(SelectedTour);
                timer.StopTimer();
               var x= timer.IsTimerEnabled;
           //     timer = null;
				//timer = DependencyService.Get<IAdvancedTimer>();
                timer.InitTimer(1000,UpdatePosition, true);
                timer.StartTimer();
			});
        }
        public void OnPlceNumberTap(object sender, EventArgs e)
        {
            var ojbPlceNumber = sender as Grid;
            var objData = ojbPlceNumber.BindingContext as TourLocation;
            SelectedTour = objData;
            routeCoversListBindData(System.Int32.Parse(objData.placeNumberTitle) - 1);
            if(player!= null)
                player.Stop();
            UpdateScreenData(objData);
            if (SelectedTour.location_audio == "")
            { 
                timer.StopTimer();
               
                AudioPlayerGrid.IsVisible = false;
				stckDownload.IsVisible = false;

            }
            else
            {
				AudioPlayerGrid.IsVisible = true;
				stckDownload.IsVisible = true;

                if (timer != null)
                {
                    timer.StopTimer();
                    timer = null;
                    timer = DependencyService.Get<IAdvancedTimer>();
                    timer.InitTimer(3000, timerElapsed, false);
                    timer.StartTimer();
                }
            }
		    SelectTourIndex = System.Convert.ToInt32(objData.placeNumberTitle);
		}
        public void UpdateScreenData(TourLocation selectedTour)
        {
            lblTourName.Text = selectedTour.location_name.ToUpper();
            lblTourDescription.Text = Regex.Replace(selectedTour.location_text, "<.*?>", string.Empty);

            sliderPositionAudio.Value = 0;
         
            imgMute.Source = "route_volume_icon.png";
            imgPausePlay.Source = "play_icon.png";

            BufferingAudioStatus(true);
            AudioPlaying = false;
           

            stckDownload.BindingContext = selectedTour;
            imgPausePlay.BindingContext = selectedTour;
            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
               if (SelectTourIndex > 0)
               {
                   StackLayout c = lstPlaceNumber.Content as StackLayout;
                    lstPlaceNumber.ScrollToAsync(c.Children[SelectTourIndex - 1], ScrollToPosition.Start, false);
                   c.Children[SelectTourIndex - 1].Focus();
               }
               return false;
            });
        }
        public void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var x = sender as ListView;
            x.SelectedItem = null;
        }
        #endregion

        #region Audio player variables
        ISimpleAudioPlayer player;
        double CurrentVolume;
        bool AudioPlaying = false;
       
        #endregion

        #region Audio Player methods
        public void PlayerSlider_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (sliderPositionAudio.Value != player.Duration)
                player.Seek(sliderPositionAudio.Value);
        }
        public void OnImgPausePlayTap(object sender, EventArgs e)
        {
			var objAudioDataSender = sender as Image;
            var SelectedTourAudio = objAudioDataSender.BindingContext as TourLocation;
            Xamarin.Forms.FileImageSource objFileImageSource = (Xamarin.Forms.FileImageSource)imgPausePlay.Source;
            if (objFileImageSource.File == "play_icon.png")
            {
                UpdateCurrentTimeofAudio();
                player.Play();
                imgPausePlay.Source = "pause_icon.png";
            }
            else
            {
                player.Pause();
                imgPausePlay.Source = "play_icon.png";
            }
        }
        public void LoadAudioFile(TourLocation SelectedTourAudio)
        {
            if (AudioPlaying == false)
            {
                sliderPositionAudio.Value = 0;
                lblAudioDuration.Text = "0:00";
                imgMute.Source = "route_volume_icon.png";

                player = null;
                player = DependencyService.Get<ISimpleAudioPlayer>();
                player.Load(SelectedTourAudio.location_audio);
                AudioPlaying = true;
               // Device.StartTimer(TimeSpan.FromSeconds(1), UpdatePosition);
            }
        }
       public void UpdatePosition(object o, EventArgs e)
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                UpdateCurrentTimeofAudio();

                sliderPositionAudio.ValueChanged -= PlayerSlider_ValueChanged;
                sliderPositionAudio.Value = player.CurrentPosition;
                sliderPositionAudio.ValueChanged += PlayerSlider_ValueChanged;
            });
         //   return AudioPlaying;
        }

        public void UpdateCurrentTimeofAudio()
        {
            if (!Double.IsNaN(player.Duration) && !Double.IsInfinity(player.Duration))
            {
                int CurrentDuration = (int)(player.Duration - player.CurrentPosition);

                int seconds = CurrentDuration % 60;
                int minutes = CurrentDuration / 60;
                string time = minutes + ":" + seconds;
                if (CurrentDuration > 0)
                    lblAudioDuration.Text = "-" + time.ToString();
                else
                    lblAudioDuration.Text = "0:00";
                #region Audio Buffering
            
                if (player.Duration > 0)
                 {
                      sliderPositionAudio.Maximum = player.Duration;
                      sliderPositionAudio.IsEnabled = player.CanSeek;
                       BufferingAudioStatus(false);
                 }
             
                #endregion

            }
        }
        public void BufferingAudioStatus(bool AudioLoadingStatus)
        {
			BufferingAudio.IsRunning = AudioLoadingStatus;
			AudioPlayerBufferingPanel.IsVisible = AudioLoadingStatus;
			lblAudioDuration.IsVisible = AudioLoadingStatus ? false : true;
          /*  if (BufferingAudio.IsRunning == true)
            {
                BufferingAudio.IsRunning = false;
                AudioPlayerBufferingPanel.IsVisible = false;
                lblAudioDuration.IsVisible = false;
            }
            else
            {
                BufferingAudio.IsRunning = true;
                AudioPlayerBufferingPanel.IsVisible = true;
                lblAudioDuration.IsVisible = false;
            }*/
		}
		public void OnImgMuteTap(object sender, EventArgs e)
		{
			if (player.Volume != 0)
			{
				CurrentVolume = player.Volume;
				player.Volume = 0;
				imgMute.Source = "route_mute_volume_icon";
			}
			else
			{
				player.Volume = CurrentVolume;
				imgMute.Source = "route_volume_icon";
			}
		}
		public async void OnImgDownloadTap(object sender, EventArgs e)
		{
			var objAudioDataSender = sender as StackLayout;
			var SelectedTourAudio = objAudioDataSender.BindingContext as TourLocation;
            await DisplayAlert(AppResources.AppResources.LMessage, SelectedTourAudio.location_audio+" Downloading..", AppResources.AppResources.LOk);
			Xamarin.Forms.DependencyService.Register<IAudioDownlaod>();
			DependencyService.Get<IAudioDownlaod>().Downlaod_mp3_Url(SelectedTourAudio.location_audio, SelectedTourAudio.location_name + ".mp3", loadingDownloadAudio, lblDownloadingSts);
		}
		protected override void OnDisappearing()
		{
            AudioPlaying = false;
			if (player != null)
            {
                if ((player.IsPlaying == true) && (player != null))
					player.Stop();
			}
            if(timer != null)
            {
                timer.StopTimer();
            }
            BufferingAudioStatus(false);
		}

		#endregion

		#region Add Pins in Map
		public void MapBindpinsInMap(VadodaraByFoot.ServiceLayer.ServiceClass.MapResponse.RootObject response)
		{
			for (int i = 0; i < _routeCovers.Count; i++)
			{
				var pin = new Pin();
				pin.Position = new Position(Convert.ToDouble(_routeCovers[i].location_latitude), Convert.ToDouble(_routeCovers[i].location_longitude));
				pin.Label = (i + 1).ToString() + " " + _routeCovers[i].location_name;
				pin.Address = "";
				pin.Type = PinType.SavedPin;
				customMap.Pins.Add(pin);
			}

			customMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(customMap.Pins[0].Position.Latitude, customMap.Pins[0].Position.Longitude), Xamarin.Forms.Maps.Distance.FromMiles(7.0)));
			customMap.IsShowingUser = true;
		}
		#endregion
	}
}
