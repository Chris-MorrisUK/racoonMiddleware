using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public struct LDLGeoPoint
    {
        public float Latitude;
        public float Longitude;

        public string ToSparql()
        {
            return string.Format("\"Point({0} {1})\"^^geo:wktLiteral", Longitude, Latitude);
        }
    }
}
