using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using Android.Util;

namespace VadodaraByFoot.Droid
{
    [Activity(Label = "VadodaraByFoot",
               Theme = "@style/MyTheme",
               Icon = "@drawable/icon",
               ScreenOrientation = ScreenOrientation.Portrait,
               ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity

    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            #region Android Theme Changes
            // Set ActionBar app icon
            this.ActionBar.SetIcon(Android.Resource.Color.Transparent);
            Window window = this.Window;

            // Set Status bar color 
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.SetStatusBarColor(Color.ParseColor("#018AB5"));
        
	        //Set Actionbar title color
            TitleColor = Color.White;
            //Window window = this.Window;
            #endregion
            Window.SetSoftInputMode(Android.Views.SoftInput.AdjustResize);

            getDeviceSize();
            getActionbarHeight();
            DependencyService.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			Xamarin.FormsMaps.Init(this, bundle);
           
			global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        public void getDeviceSize()
        {
			var metrics = Resources.DisplayMetrics;
			int widthInDp = (int)(metrics.WidthPixels / metrics.Density);
			int heightInDp = (int)(metrics.HeightPixels / metrics.Density);
            AppStatics.DeviceWidth = widthInDp;
            AppStatics.DeviceHeight = heightInDp;


			int density = (int)Resources.DisplayMetrics.Density;

			switch (density)
			{
				case (int)0.75:
                    AppStatics.AndroidScreenResolutionType = "ldpi"; //120dpi
					break;
				case (int)1.0:
					AppStatics.AndroidScreenResolutionType = "mdpi"; //160dpi
					break;
				/*case (int)1.5:
					AppStatics.AndroidScreenResolutionType = "hdpi";//240dpi
					break;*/
				case (int)2.0:
					AppStatics.AndroidScreenResolutionType = "xhdpi";//320 dpi
					break;
			/*	case (int)2.5:
					AppStatics.AndroidScreenResolutionType = "xhdpi";//320 dpi
					break;/*
                case (int)3.0:
                    AppStatics.AndroidScreenResolutionType = "xxhdpi";//480dpi
                    break;
			/*	case (int)3.5:
					AppStatics.AndroidScreenResolutionType = "xxhdpi";//560dpi
					break;*/
				case (int)4.0:
                    AppStatics.AndroidScreenResolutionType = "xxxhdpi";//640dpi
                    break;
			/*	case (int)4.5:
					AppStatics.AndroidScreenResolutionType = "xxxhdpi";//640dpi
					break;*/
             }
        }
        public void getActionbarHeight()
        {
			// Calculate ActionBar height
            var metrics = Resources.DisplayMetrics;
			TypedValue tv = new TypedValue();
            if(Theme.ResolveAttribute(Android.Resource.Attribute.ActionBarSize, tv, true))
            {
               var actionbarH= TypedValue.ComplexToDimensionPixelSize(tv.Data, Resources.DisplayMetrics);
                AppStatics.ActionBarHeight = (int)(actionbarH / metrics.Density);
            }
        }
    }
}

