using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLPointsNode:LDLSandCBASE
    {

        public LDLPointsNode(string id)
            : base(id)
        { 
            
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.PointsTypeUri;
            }
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.PointsType;
            }
        }

        public string PNType; //arguably this could be an enum

    }
}
