using MiddleWareBussinessObjects;
using RacoonMiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonServices
{
    [DataContract]
    [KnownType(typeof(LabelledItem))]
    public class LabelledItemExtended : LabelledItem, IPopulateFromBO
    {
        public LabelledItemExtended()
		{ 

		}
        public LabelledItemExtended(string uri, string label,string comment,string graph):base(uri,label)
        {            
            ItemComment = comment;
            ItemGraph = new Uri(graph);
        }

		[DataMember]
        public string   ItemComment
		{
			get;
			private set;
		}

		[DataMember]
        public Uri ItemGraph
		{
			get;
			private set;
		}

		public new void Populate(IMappableBussinessObject bo)
		{
            NamedThingDetail boversion = bo as NamedThingDetail;
			if (boversion == null)
                throw new ArgumentException("It is only possible to create a Labeled Item from a NamedThingDetail");
			base.Label = boversion.Label;
			base.ItemUri = boversion.ItemUri;
            ItemComment = boversion.ItemComment;
            ItemGraph = boversion.ItemGraph;
		}
    }
}