using VadodaraByFoot.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Editor), typeof(CustomEditorRenderer_Droid))]
namespace VadodaraByFoot.Droid.Renderers
{
    public class CustomEditorRenderer_Droid : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }
    }
}
