using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace VadodaraByFoot.CustomControls
{
    public class Gradient_Stack : StackLayout
    {
		public static readonly BindableProperty StartColorProperty = BindableProperty.Create
		  (
			   "StartColor",
			   typeof(Color),
			   typeof(Gradient_Stack),
			   Color.Transparent
		  );

		public Color StartColor
		{
			get { return (Color)GetValue(StartColorProperty); }
			set { SetValue(StartColorProperty, value); }
		}
		public static readonly BindableProperty EndColorProperty = BindableProperty.Create
		  (
			   "EndColor",
			   typeof(Color),
			   typeof(Gradient_Stack),
			   Color.Transparent
		  );

		public Color EndColor
		{
			get { return (Color)GetValue(EndColorProperty); }
			set { SetValue(EndColorProperty, value); }
		}
    }
}
