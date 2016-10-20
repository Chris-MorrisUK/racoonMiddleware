using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
	public class TaskOntology : IMappableBussinessObject
	{
		public TaskOntology()
		{ 
			//Nothing needs to be done in this case, but I was envisaging a public constructor
		}
		
		public List<KeyValuePair<string, string>> SimpleMappings
		{
			get
			{
				return new List<KeyValuePair<string, string>>() { 
					new KeyValuePair<string,string>("s" ,"BaseUri"),
					new KeyValuePair<string,string>("lab" ,"OntologyName"),
				};
			}
		}

		public void DoComplexMappings(IEnumerable<MiddlewareParameter> toMap)
		{
			foreach (MiddlewareParameter param in toMap)
			{
				if ((param.ParamName == "comment") || (param.ParamName == "desc"))
				{
					if (string.IsNullOrEmpty(Description))
						Description = ((MiddlewareParameter<string>)param).ParamValue;
					else
						Description = " " + ((MiddlewareParameter<string>)param).ParamValue;
				}
			}
		}
		#region fields and properties

		private string ontologyName;

		public string OntologyName
		{
			get { return ontologyName; }
			set { ontologyName = value; }
		}

		private Uri baseUri;

		public Uri BaseUri
		{
			get { return baseUri; }
			set { baseUri = value; }
		}

		private string description;

		public string Description
		{
			get { return description; }
			set { description = value; }
		}
		#endregion

	}
}
