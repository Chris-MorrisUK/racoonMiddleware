using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackCircuit : LDLBOBase
    {
        public LDLTrackCircuit(string id)
            : base(id)
        { }

        public static string Identitifier = "TRACK_CIRCUIT";
        public LDLInterlocking Interlocking;
        public string InterlockingStr;
        public LDLTrackCircuitTypeEnum TCType;
        public LDLSectionList Sections;//It's a multi-part section, not a section list technically, for my purposes it seems OK to model them identically 

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {            
            if (!string.IsNullOrEmpty(InterlockingStr))
            {
                LDLBOBase InterlockingObj;
                parsedObjects.TryGetValue(InterlockingStr, out InterlockingObj);
                Interlocking = InterlockingObj as LDLInterlocking;
            }
            Sections.DoSecondPass(parsedObjects);
        }
    }
}
