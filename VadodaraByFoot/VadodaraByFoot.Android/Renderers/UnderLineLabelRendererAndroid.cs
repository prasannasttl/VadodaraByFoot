using System;
using Xamarin.Forms;
using VadodaraByFoot.Droid.Renderers;
using VadodaraByFoot.CustomControls;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using Android.Graphics;

[assembly: ExportRenderer(typeof(UnderLineLabel), typeof(UnderLineLabelRendererAndroid))]
namespace VadodaraByFoot.Droid.Renderers
{
	public class UnderLineLabelRendererAndroid : LabelRenderer
	{
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);
			try
			{
				var textView = (TextView)Control;
				textView.PaintFlags |= PaintFlags.UnderlineText;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
			}
		}
	}
}