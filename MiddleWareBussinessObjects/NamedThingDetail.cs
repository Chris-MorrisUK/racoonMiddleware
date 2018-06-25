using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    public class NamedThingDetail : NamedThing, IMappableBussinessObject
    {
        public NamedThingDetail()
        {

        }
        public NamedThingDetail(Uri uri, string label,string comment,Uri graph):base(uri,label)
        {            
            ItemComment = comment;
            ItemGraph = graph;
        }

        public new List<KeyValuePair<string, string>> SimpleMappings
        {
            get
            {
                /* 
                 * I'm sure this is recorded as a comment else where but the correct 
                 * order is: Parameter (from SPARQL), PropertyName (C#)
                 */
                return new List<KeyValuePair<string, string>>() { 
					new KeyValuePair<string,string>("ItemUri" ,"ItemUri"),
					new KeyValuePair<string,string>("Label" ,"Label"),
                    new KeyValuePair<string,string>("ItemComment" ,"ItemComment"),
					new KeyValuePair<string,string>("graph" ,"ItemGraph"),
				};
            }
        }
        

        public new void DoComplexMappings(IEnumerable<MiddlewareParameter> toMap)
        {
            base.DoComplexMappings(toMap);
        }


        public string ItemComment
        {
            get;
            set;
        }


        public Uri ItemGraph
        {
            get;
            set;
        }
    }
}
