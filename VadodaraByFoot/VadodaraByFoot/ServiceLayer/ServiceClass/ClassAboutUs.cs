using System;
using VadodaraByFoot.ServiceLayer.ServiceClass;

namespace VadodaraByFoot.ServiceLayer.ServiceClass
{
	/*public class Links
	{
		public string self { get; set; }
		public string author { get; set; }
		public string collection { get; set; }
		public string replies { get; set; }
		public string __invalid_name__version-history { get; set; }
    }

	public class Meta
	{
		public Links links { get; set; }
	}*/

	public class RootObjectAboutUs
	{
		public string _resultflag { get; set; }
		public string message { get; set; }
		public string ID { get; set; }
		public string title { get; set; }
		public string is_image { get; set; }
		public string content { get; set; }
		//public Meta meta { get; set; }
	}

	
}

