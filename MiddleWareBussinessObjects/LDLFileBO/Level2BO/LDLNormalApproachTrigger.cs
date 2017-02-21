using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLNormalApproachTrigger : LDLBOBase,    LDLIHasDirectedLocation
    {
        private const string identity = "NORMAL_APPROACH_TRIGGER";

        public static string Identity
        {
            get {
                return identity;
            }
        }

        public LDLNormalApproachTrigger(string id)
            : base(id)
        { }

        public LDLDirectedLocation Location { get; set; }
        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            Location.DoSecondPass(parsedObjects);
        }
    }
}
