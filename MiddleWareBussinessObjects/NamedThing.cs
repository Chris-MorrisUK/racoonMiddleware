using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
	public class NamedThing :IMappableBussinessObject
	{
		public NamedThing()
		{ 

		}
		public NamedThing(Uri uri, string label)
		{
			ItemUri = uri;
			Label = label;
		}

		public List<KeyValuePair<string, string>> SimpleMappings
		{
			get
			{
				return new List<KeyValuePair<string, string>>() { 
					new KeyValuePair<string,string>("type" ,"ItemUri"),
					new KeyValuePair<string,string>("label" ,"Label"),
				};
			}
		}

		public void DoComplexMappings(IEnumerable<MiddlewareParameter> toMap)
		{
			//nothing needs doing here - all the mappings are simple
		}


		public Uri ItemUri
		{
			get;
			set;
		}


		public string Label
		{
			get;
			set;
		}
	}
}
