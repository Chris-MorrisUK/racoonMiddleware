using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public enum BaliseDuplicateStatus
    {
        NO_DUPLICATES,
        DUPLICATE_OF_THE_NEXT_BALISE,
        DUPLICATE_OF_THE_PREVIOUS_BALISE,
        SPARE
    }
}
