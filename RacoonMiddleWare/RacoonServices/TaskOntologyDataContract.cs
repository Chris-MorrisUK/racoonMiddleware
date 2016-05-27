using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MiddleWareBussinessObjects;
using RacoonMiddleWare;

namespace RacoonServices
{
	[DataContract]
	public class TaskOntologyDataContract : IPopulateFromBO
	{
		public TaskOntologyDataContract()
		{ }


	
		public TaskOntologyDataContract(string _uri, string _name, string _desc)
		{
			baseUri = _uri;
			name = _name;
			desc = _desc;
		}

		private string baseUri;
		private string name;
		private string desc;

		[DataMember]
		public string Description
		{
			get { return desc; }
			set { desc = value; }
		}

		[DataMember]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[DataMember]
		public string BaseUri
		{
			get { return baseUri; }
			set { baseUri = value; }
		}

		public void Populate(IMappableBussinessObject bo)
		{
			TaskOntology source = bo as TaskOntology;
			if (source == null)
				throw new ArgumentException("It is only possible to create a TaskongOntologyDataContract from a TaskOntology");
			this.baseUri = source.BaseUri;
			this.Description = source.Description;
			this.Name = source.OntologyName;
		}
	}
}