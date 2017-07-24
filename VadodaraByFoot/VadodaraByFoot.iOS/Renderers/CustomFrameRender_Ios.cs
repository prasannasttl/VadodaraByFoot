using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;

using VadodaraByFoot.iOS.Renderers;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameRenderer_Ios))]
namespace VadodaraByFoot.iOS.Renderers
{
	public class CustomFrameRenderer_Ios : FrameRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);
			if (e.NewElement != null)
				SetupLayer();

            if (Element != null)
			    Element.HasShadow = false;
            
			else
            {
				if (Element != null)
					Element.HasShadow = false;
			}
		}
		void SetupLayer()
		{
			Layer.BorderColor = Element.OutlineColor.ToCGColor();
		//	Layer.BorderWidth = 1;
			Layer.RasterizationScale = UIScreen.MainScreen.Scale;
			Layer.ShouldRasterize = true;
		}
	}
}