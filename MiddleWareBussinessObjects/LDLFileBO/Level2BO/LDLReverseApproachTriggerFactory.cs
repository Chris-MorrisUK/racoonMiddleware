using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLReverseApproachTriggerFactory : LDLApproachTriggerFactory, IParsableFactory
    {
        public string Identifier
        {
            get { return LDLReverseApproachTrigger.Identity; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLReverseApproachTrigger trigger = new LDLReverseApproachTrigger(id);
            LDLApproachTriggerFactory.CreateTrigger(definition[0], trigger);
            return trigger;
        }
    }
}
