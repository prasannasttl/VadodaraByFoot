using System;
using System.Collections.Generic;

namespace VadodaraByFoot.ServiceLayer.ServiceClass
{
    public class Walktour
    {
        public string ID { get; set; }
        public string title { get; set; }
        public string is_image { get; set; }
        public int TitleFontSize { get; set; }
        public Xamarin.Forms.Color EndColorGradient { get; set; }
        public Xamarin.Forms.Color StartColorGradient { get; set; }
    }

	public class RootObjectWalkTourList
	{
		public string _resultflag { get; set; }
		public string message { get; set; }
		public List<Walktour> Walktour { get; set; }
	}
}
