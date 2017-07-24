

using VadodaraByFoot.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(Button), typeof(ButtonRendererAndroid))]
namespace VadodaraByFoot.Droid.Renderers
{
    public class ButtonRendererAndroid : ButtonRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);
			
            var button = (Android.Widget.Button)this.Control;
            button.SetAllCaps(false);
         }
    }
}

