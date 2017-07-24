using Android.OS;
using Android.Views.InputMethods;
using VadodaraByFoot.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(Entry), typeof(EntryRendererAndroid))]
namespace VadodaraByFoot.Droid.Renderers
{
    public class EntryRendererAndroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
			Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
			//Control.color
          //  Control?.SetCursorVisible(true);
            Control.TextAlignment = Android.Views.TextAlignment.Center;
            Control.SetPadding(10, 10, 10, 10);
           // Element.VerticalOptions = LayoutOptions.CenterAndExpand;
            //  Control.SetPadding(10, 10, 10, 10);
            Control.Gravity = Android.Views.GravityFlags.CenterVertical;

			/*Control.Click += (sender, evt) =>
			{
				new Handler().Post(delegate
					{
						var imm = (InputMethodManager)Control.Context.GetSystemService(Android.Content.Context.InputMethodService);
						var result = imm.HideSoftInputFromWindow(Control.WindowToken, 0);
                    					
					});
			};

			Control.FocusChange += (sender, evt) =>
			{
				new Handler().Post(delegate
					{
						var imm = (InputMethodManager)Control.Context.GetSystemService(Android.Content.Context.InputMethodService);
						var result = imm.HideSoftInputFromWindow(Control.WindowToken, 0);

					});
			};*/

        }
    }
}
