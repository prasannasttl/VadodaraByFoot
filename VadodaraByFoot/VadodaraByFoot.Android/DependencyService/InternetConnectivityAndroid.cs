using System;
using VadodaraByFoot.Interface;
using VadodaraByFoot.Droid.DependencyService;
using Android.Content;

[assembly: Xamarin.Forms.Dependency(typeof(InternetConnectivityAndroid))]
namespace VadodaraByFoot.Droid.DependencyService
{
   	public class InternetConnectivityAndroid : IInternetConnectivity
	{
		public bool IsInternetConnected()
		{
			var connectivityManager = (Android.Net.ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
			var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
			return (activeNetworkInfo != null && activeNetworkInfo.IsConnectedOrConnecting);
		}
	}
}
