using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public interface ILDL_GPSLocated
    {
        List<LDLGPSPoint> Locations { get; }
    }
}
