 using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System;
using VadodaraByFoot.Droid.Renderers;
using VadodaraByFoot.CustomControls;

[assembly: ExportRenderer(typeof(Gradient_Stack), typeof(GradientStackAndroid))]
namespace VadodaraByFoot.Droid.Renderers
{
    public class GradientStackAndroid : Xamarin.Forms.Platform.Android.VisualElementRenderer<StackLayout>
    {
		private Color StartColor { get; set; }
		private Color EndColor { get; set; }

		protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
		{
			var gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,
			this.StartColor.ToAndroid(),
			this.EndColor.ToAndroid(),
			Android.Graphics.Shader.TileMode.Mirror);
			var paint = new Android.Graphics.Paint()
			{
				Dither = true,
			};
			paint.SetShader(gradient);
			canvas.DrawPaint(paint);
			base.DispatchDraw(canvas);
		}
		protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement != null || Element == null)
			{
				return;
			}
			try
			{
				var stack = e.NewElement as Gradient_Stack;
                this.StartColor =stack.StartColor;
                this.EndColor = stack.EndColor;
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
			}
		}
    }
}