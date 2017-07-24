using System;
using Xamarin.Forms;
using VadodaraByFoot.Droid.Renderers;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Content.Res;

[assembly:ExportRenderer(typeof(Slider), typeof(CustomSliderRendererAndroid))]
namespace VadodaraByFoot.Droid.Renderers
{
	public class CustomSliderRendererAndroid : SliderRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				// Set Progress bar Thumb color
			/*	Control.Thumb.SetColorFilter(
					Xamarin.Forms.Color.FromHex("#ffffff").ToAndroid(),
					PorterDuff.Mode.SrcIn);*/
                // set progress bar line color 

				Control.ProgressBackgroundTintList
		   = ColorStateList.ValueOf(
			Xamarin.Forms.Color.FromHex("#222222").ToAndroid());
				Control.ProgressBackgroundTintMode
					   = PorterDuff.Mode.SrcIn;
                
			}
		}
	}
}