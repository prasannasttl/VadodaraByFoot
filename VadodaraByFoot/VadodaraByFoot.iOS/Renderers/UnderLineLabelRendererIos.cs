using System;
using Foundation;
using UIKit;
using VadodaraByFoot.CustomControls;
using VadodaraByFoot.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(UnderLineLabel), typeof(UnderLineLabelRendererIos))]
namespace VadodaraByFoot.iOS.Renderers
{
	public class UnderLineLabelRendererIos : LabelRenderer
	{
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
			base.OnElementChanged(e);
			try
			{
				var label = (UILabel)Control;
				var text = (NSMutableAttributedString)label.AttributedText;
				var range = new NSRange(0, text.Length);
				text.AddAttribute(UIStringAttributeKey.UnderlineStyle, NSNumber.FromInt32((int)NSUnderlineStyle.Single), range);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot underline Label. Error: ", ex.Message);
			}
        }
      
	}
}