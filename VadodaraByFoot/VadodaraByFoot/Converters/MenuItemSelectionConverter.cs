using System;
using Xamarin.Forms;

namespace VadodaraByFoot.Converters
{
   public class MenuItemSelectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int IsselectedValue = System.Convert.ToInt32(value);
            if (IsselectedValue == 1)return 1;
                else return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
			int IsselectedValue = System.Convert.ToInt32(value);
			if (IsselectedValue == 1) return 1;
			else return 0;
		}
    }
}
