using System;
using Xamarin.Forms;
namespace VadodaraByFoot
{
    // User isolated storage class
    public class UserData
    {
        public string username { get; set; }
		public string name { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string nickname { get; set; }
		public string slug { get; set; }
		public string URL { get; set; }
		public string avatar { get; set; }
		public string mobile { get; set; }
		public string description { get; set; }
		public string registered { get; set; }
		public string email { get; set; }
        public int ID { get; set; }
    }

    // Route covers page top list class
    public class RouteCoversNumbersList
    {
        public ImageSource placeNumberRing { get; set; }
        public string placeNumberTitle { get; set; }
        public double SeparatorWidth { get; set; }
    }

    // menu drawer list item class
	public class MenuItem
	{
		public ImageSource Icon { get; set; }
		public string Title { get; set; }
		public string Tap { get; set; }
        public Double IsSelected { get; set; }
		public Type TargetType { get; set; }
		public int TitleFontSize { get; set; }
	}
}
