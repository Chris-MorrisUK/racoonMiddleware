using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLRBC : LDLBOBase
    {
        public LDLRBC(string id)
            : base(id)
        { 
        }
        private const string identity = "RBC";
        public static string Identity
        {
            get
            {
                return identity;
            }
        }

        public override Uri ObjectUri
        {
            get
            {
                return LDLUris.RadioBlockControlCentreTypeUri;
            }
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.RadioBlockControlCentreTypeStr;
            }
        }
       
    }
}
