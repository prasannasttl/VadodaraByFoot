using System;
namespace VadodaraByFoot.ServiceLayer.ServiceClass
{
		/*public class Capabilities
		{
			public bool read { get; set; }
			public bool level_0 { get; set; }
			public bool edit_posts { get; set; }
			public bool subscriber { get; set; }
		}

		public class Links
		{
			public string self { get; set; }
			public string archives { get; set; }
		}

		public class Meta
		{
			public Links links { get; set; }
		}*/

		public class RootObjectUserLogin
		{
			public string _resultflag { get; set; }
			public string message { get; set; }
			public int ID { get; set; }
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
			//public List<string> roles { get; set; }
			//public Capabilities capabilities { get; set; }
			public string email { get; set; }
			//public Meta meta { get; set; }
		}
	}

