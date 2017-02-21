using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    [Flags]
    public enum LDLSignalAspect
    {
        RED, 
        YELLOW,
        GREEN,
        YYELLOW,
        FLASHING_YELLOW,
        FLASHING_YYELLOW,
        WHITE,
        FLASHING_RED,
        REPEATED_YYELLOW,
        FLASHING_GREEN,
        ERTMS,
        ON,
        OFF,
        POSA,
        SUBSIDIARY
    }
}
