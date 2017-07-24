using System;
using VadodaraByFoot.Interface;
using VadodaraByFoot.Droid.DependencyService;
using Android.Views.InputMethods;
using Android.App;
using Android.Content;

[assembly: Xamarin.Forms.Dependency(typeof(DissmissKeyboardAndroid))]
namespace VadodaraByFoot.Droid.DependencyService
{
	public class DissmissKeyboardAndroid : IDissmissKeyboard
	{
		public void DismissKeyboard()
		{
			var inputMethodManager = Xamarin.Forms.Forms.Context.GetSystemService(Context.InputMethodService) as InputMethodManager;
			if (inputMethodManager != null && Xamarin.Forms.Forms.Context is Activity)
			{
				var activity = Xamarin.Forms.Forms.Context as Activity;
				var token = activity.CurrentFocus == null ? null : activity.CurrentFocus.WindowToken;
				inputMethodManager.HideSoftInputFromWindow(token, 0);
			}
		}
	}
}