using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLReverseApproachTrigger : LDLBOBase,  LDLIHasDirectedLocation
    {
        public LDLReverseApproachTrigger(string id)
            : base(id)
        { }


        private const string identity = "REVERSE_APPROACH_TRIGGER";

        public static string Identity
        {
            get
            {
                return identity;
            }
        }

        public LDLDirectedLocation Location { get; set; }
        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            Location.DoSecondPass(parsedObjects);
        }
    }
}
