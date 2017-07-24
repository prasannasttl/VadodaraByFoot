using System;
using System.Collections.Generic;
using VadodaraByFoot.ServiceLayer.ServiceClass;
using Xamarin.Forms;

/*namespace VadodaraByFoot.ServiceLayer.ServiceClass
{
	public class TourLocation
	{
		public string location_name { get; set; }
		public string location_latitude { get; set; }
		public string location_longitude { get; set; }
		public string location_order { get; set; }
		public string location_text { get; set; }
		public string location_audio { get; set; }
	}

	public class Thumbnail
	{
		public string file { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string url { get; set; }
	}

	public class Medium
	{
		public string file { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string url { get; set; }
	}

	public class TwentyseventeenThumbnailAvatar
	{
		public string file { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string url { get; set; }
	}

	public class MapImage
	{
		public string file { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string url { get; set; }
	}

	public class TourTimeline
	{
		public string file { get; set; }
		public int width { get; set; }
		public int height { get; set; }
		public string url { get; set; }
	}

	public class FeaturedImage
	{
		public string _resultflag { get; set; }
		public string message { get; set; }
		public string ID { get; set; }
		public string title { get; set; }
		public string status { get; set; }
		public string type { get; set; }
		public string content { get; set; }
		public string parent { get; set; }
		public string link { get; set; }
		public string date { get; set; }
		public string modified { get; set; }
		public string format { get; set; }
		public string slug { get; set; }
		public string excerpt { get; set; }
		public string menu_order { get; set; }
		public string comment_status { get; set; }
		public string ping_status { get; set; }
		public string date_tz { get; set; }
		public string date_gmt { get; set; }
		public string modified_tz { get; set; }
		public string modified_gmt { get; set; }
		public string password { get; set; }
		public string source { get; set; }
	}
	public class Meta2
	{
		public List<string> registration_on_off { get; set; }
		public string tour_place { get; set; }
		public string tour_place_latitude { get; set; }
		public string tour_place_longitude { get; set; }
		public string tour_fees { get; set; }
		public List<TourLocation> tour_location { get; set; }
	}
	public class RootObjectWalkTourDetail
	{
		public string _resultflag { get; set; }
		public string message { get; set; }
		public string ID { get; set; }
		public string title { get; set; }
		public string status { get; set; }
		public string type { get; set; }
		public string content { get; set; }
		public object parent { get; set; }
		public string link { get; set; }
		public string date { get; set; }
		public string modified { get; set; }
		public string format { get; set; }
		public string slug { get; set; }
		public string excerpt { get; set; }
		public string menu_order { get; set; }
		public string comment_status { get; set; }
		public string ping_status { get; set; }
		public string date_tz { get; set; }
		public string date_gmt { get; set; }
		public string modified_tz { get; set; }
		public string modified_gmt { get; set; }
		public string password { get; set; }
		public FeaturedImage featured_image { get; set; }
		public Meta2 meta { get; set; }
	}


}*/


namespace VadodaraByFoot.ServiceLayer.ServiceClass
{
    public class TourLocation
    {
        public string location_name { get; set; }
        public string location_latitude { get; set; }
        public string location_longitude { get; set; }
        public string location_order { get; set; }
        public string location_text { get; set; }
        public string location_image { get; set; }
        public string location_audio { get; set; }
        #region Route covers page top list class
        public ImageSource placeNumberRing { get; set; }
        public string placeNumberTitle { get; set; }
        public double SeparatorWidth { get; set; }
        public int NumberFontSize { get; set; }
        public double SpacingBetweenNumber { get; set; }
        #endregion
    }

	public class Meta
	{
		public string tour_place { get; set; }
		public List<TourLocation> tour_location { get; set; }
		public string tour_place_latitude { get; set; }
		public string tour_place_longitude { get; set; }
	}

	public class Walktourdetail
	{
		public string ID { get; set; }
		public string title { get; set; }
		public string content { get; set; }
		public string excerpt { get; set; }
		public Meta meta { get; set; }
	}

	public class RootObjectWalkTourDetail
	{
		public string _resultflag { get; set; }
		public string message { get; set; }
		public Walktourdetail Walktourdetail { get; set; }
	}
}