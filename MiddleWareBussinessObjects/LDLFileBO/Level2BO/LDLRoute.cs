using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLRoute : LDLBOBase 
    {

        public LDLRoute(string id)
            : base(id)
        { }
        public LDLRouteType RouteType;
        public LDLInterlocking Interlocking;
        public string InterlockingStr;

        public string Overlap_ID;
        


    }
}
