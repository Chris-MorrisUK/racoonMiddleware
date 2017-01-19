using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public enum LDLBaliseGroupType
    {
        SIGNAL,
        ERTMS_LEVEL_TRANSITION,
        LT_ANNOUNCEMENT,
        IN_FILL,
        MAIN,
        LEVEL_CROSSING,
        RBC_BOUNDARY,
        ODOMETRY
    }
}
