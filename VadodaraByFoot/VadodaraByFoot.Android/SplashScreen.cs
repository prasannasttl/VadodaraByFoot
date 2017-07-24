using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace VadodaraByFoot.Droid
{
    [Activity(Label = "VadodaraByFoot",Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Splash);

        //    System.Threading.Thread.Sleep(500);
            this.StartActivity(typeof(MainActivity));
        }
    }
}