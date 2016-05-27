using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using MiddleWareBussinessObjects;

namespace RacoonMiddleWare
{
	[DataContract]
	public class LabelledItem: IPopulateFromBO
	{
		public LabelledItem()
		{ 

		}
		public LabelledItem(string uri, string label)
		{
			ItemUri = new Uri(uri);
			Label = label;
		}

		[DataMember]
		public Uri ItemUri
		{
			get;
			private set;
		}

		[DataMember]
		public string Label
		{
			get;
			private set;
		}

		public void Populate(IMappableBussinessObject bo)
		{
			NamedThing boversion = bo as NamedThing;
			if (boversion == null)
				throw new ArgumentException("It is only possible to create a Labeled Item from a NamedThing");
			this.Label = boversion.Label;
			this.ItemUri = new Uri(boversion.ItemUri);
		}
	}
}