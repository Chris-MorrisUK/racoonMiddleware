using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLGraphBase : LDLBOBase
    {
        public LDLGraphBase(string id)
            : base(id)
        { }

        public LDLTrackEndNode TrackStart;
        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
           //Nothing to do here
        }
    }
}
