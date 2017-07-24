using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace VadodaraByFoot.ServiceLayer.ServiceClass
{
	public class Faqlist
	{
		public string ID { get; set; }
		public string title { get; set; }
		public string status { get; set; }
		public string type { get; set; }
		public string author { get; set; }
		public string content { get; set; }
		public string parent { get; set; }
		public string link { get; set; }
        public ImageSource expand_collapse { get; set; }
        public string TapId { get; set; }
        public bool ObjIsVisible { get; set; }
		public int TitleFontSize { get; set; }
        public int DetailFontSize { get; set; }
	}

	public class RootObjectFAQ
	{
		public string _resultflag { get; set; }
		public string message { get; set; }
		public List<Faqlist> faqlist { get; set; }
	}
}
