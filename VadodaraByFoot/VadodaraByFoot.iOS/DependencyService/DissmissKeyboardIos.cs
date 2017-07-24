
using System;
using UIKit;
using VadodaraByFoot.Interface;
using VadodaraByFoot.iOS.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(DissmissKeyboardIos))]
namespace VadodaraByFoot.iOS.DependencyService
{
	public class DissmissKeyboardIos : IDissmissKeyboard
	{
		public void DismissKeyboard()
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
			{
				var window = UIApplication.SharedApplication.KeyWindow;
				var vc = window.RootViewController;
				while (vc.PresentedViewController != null)
				{
					vc = vc.PresentedViewController;
				}

				vc.View.EndEditing(true);
			});

		}
	}
}