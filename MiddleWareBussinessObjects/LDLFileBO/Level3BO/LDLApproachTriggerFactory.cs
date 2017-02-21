using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLApproachTriggerFactory : LDLFactoryBase
    {
        protected static void CreateTrigger(string line, LDLIHasDirectedLocation item)
        {
            item.Location = new LDLDirectedLocation(line);            
        }
    }
}
