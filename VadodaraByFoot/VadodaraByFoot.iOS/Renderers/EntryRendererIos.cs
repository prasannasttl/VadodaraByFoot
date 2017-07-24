using System;
using VadodaraByFoot.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using System.ComponentModel;
using UIKit;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryRendererIos))]
namespace VadodaraByFoot.iOS.Renderers
{
	public class EntryRendererIos : EntryRenderer
	{
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			Control.Layer.BorderWidth = 0;
			Control.BorderStyle = UITextBorderStyle.None;
		}
	}
}