using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Widget;
using VadodaraByFoot.Droid.DependencyService;
using VadodaraByFoot.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhoneDialerAndroid))]
namespace VadodaraByFoot.Droid.DependencyService
{
    public class PhoneDialerAndroid : IDialer
    {
        public bool Dial(string number)
        {
            var context = Xamarin.Forms.Forms.Context;
            if (context == null)
                return false;

            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Android.Net.Uri.Parse("tel:" + number.Replace(" ", "")));
            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {

            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));
            if (list.Any())
                return true;

            Android.Telephony.TelephonyManager mgr = Android.Telephony.TelephonyManager.FromContext(context);
            return mgr.PhoneType != Android.Telephony.PhoneType.None;
        }
    }
}