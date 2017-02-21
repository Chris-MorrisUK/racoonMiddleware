using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSignal : LDLBOBase, LDLIHasDirectedLocation
    {
        public LDLSignal(string id)
            : base(id)
        { }

        public LDLInterlocking Interlocking;
        public string InterlockingStr;
        public LDLRBC RadioBlockControl;
        public string RadioBlockControLStr;
        public LDLSignalTypeEnum SignalType;
        public LDLSignalAspect Aspects;
        public decimal SightingDistance;
        public decimal StopPosition;
        public LDLDirectedLocation Location { get; set; }
        
        //Skipping area's not included as not part of thameslink data

        public string NormalApproachStr;
        public string ReverseApproachStr;
        public LDLNormalApproachTrigger NormalApproachTrigger;
        public LDLReverseApproachTrigger ReverseApproachTrigger;
        //aspects displayable,SUBOVERLAPS,RUNNING_TIME_TO_BERTH_TC,OVERLAP_RELEASE_TIME not used        
        //er_button_latch_id,trts_latch_id,allow_routing,aws_type_and_body also omitted because they don't exist in thameslink




        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase dlocObject;
            if (!string.IsNullOrEmpty(NormalApproachStr))
            {
                parsedObjects.TryGetValue(NormalApproachStr, out dlocObject);
                NormalApproachTrigger = dlocObject as LDLNormalApproachTrigger;
            }
            if (!string.IsNullOrEmpty(ReverseApproachStr))
            {
                parsedObjects.TryGetValue(ReverseApproachStr, out dlocObject);
                ReverseApproachTrigger = dlocObject as LDLReverseApproachTrigger;
            }
            if (!string.IsNullOrEmpty(RadioBlockControLStr))
            {
                parsedObjects.TryGetValue(RadioBlockControLStr, out dlocObject);
                RadioBlockControl = dlocObject as LDLRBC;
            }
            if (!string.IsNullOrEmpty(InterlockingStr))
            {
                parsedObjects.TryGetValue(InterlockingStr, out dlocObject);
                Interlocking = dlocObject as LDLInterlocking;
            }
            if (Location != null)
                Location.DoSecondPass(parsedObjects);
        }
    }
}
