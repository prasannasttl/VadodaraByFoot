using System;
using Android.App;
using Android.Widget;
using VadodaraByFoot.Droid.DependencyService;
using VadodaraByFoot.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(MessageAndroid))]
namespace VadodaraByFoot.Droid.DependencyService 
{
    public class MessageAndroid : IMessage
    {
		public void LongAlert(string message)
		{
			Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
		}
		public void ShortAlert(string message)
		{
			Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
		}
    }
}
