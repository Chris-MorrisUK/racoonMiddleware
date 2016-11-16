using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public interface LDLIHasDirectedLocation
    {
        LDLDirectedLocation Location { get; set; }
    }
}
