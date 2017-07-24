using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using FFImageLoading.Transformations;
using Foundation;
using UIKit;

namespace VadodaraByFoot.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
       
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			CachedImageRenderer.Init();
            var ignore = new CircleTransformation();
			Xamarin.FormsMaps.Init();

            VadodaraByFoot.iOS.DependencyService.AdvancedTimerImplementationIos.Init();

			UINavigationBar.Appearance.BarTintColor = UIColor.FromRGBA(1, 172, 226,226);
			UINavigationBar.Appearance.TintColor = UIColor.White;
			UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes()
			{
				TextColor = UIColor.White
			});
            UINavigationBar.Appearance.BarStyle = UIBarStyle.Black;
            //UINavigationBar.Appearance.BackIndicatorImage = UIImage.FromBundle("top_back_icon.png");
			//UINavigationBar.Appearance.BackIndicatorTransitionMaskImage = UIImage.FromBundle("top_back_icon.png");

            UIApplication.SharedApplication.SetStatusBarHidden(false, true);
          //  UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
            getDeviceSize();
            DependencyService.CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}

        public void getDeviceSize()
        {
			var b = UIScreen.MainScreen.Bounds;
			int h = (int)b.Height;
			int w = (int)b.Width;
			AppStatics.DeviceWidth = w;
			AppStatics.DeviceHeight = h;
        }
        public void getActionbarHeight()
        {
            
        }
    }
}
