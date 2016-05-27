using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    public class LinkedDataPredicate: IMappableBussinessObject
    {
        public string LinkLabel;
        public string LinkUri;



        public List<KeyValuePair<string, string>> SimpleMappings
        {
            get
            {
                return new List<KeyValuePair<string, string>>() { 
					new KeyValuePair<string,string>("predicate" ,"LinkUri"),
					new KeyValuePair<string,string>("PredicateLabel" ,"LinkLabel"),
				};
            }
        }

        public void DoComplexMappings(IEnumerable<MiddlewareParameter> toMap)
        {
            //Not needed
        }
    }
}
