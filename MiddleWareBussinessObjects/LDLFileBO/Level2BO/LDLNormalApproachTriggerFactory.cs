using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLNormalApproachTriggerFactory : LDLApproachTriggerFactory, IParsableFactory
    {
        public string Identifier
        {
            get { return LDLNormalApproachTrigger.Identity; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLNormalApproachTrigger trigger = new LDLNormalApproachTrigger(id);
            LDLApproachTriggerFactory.CreateTrigger(definition[0],trigger);
            return trigger;
        }
    }
}
