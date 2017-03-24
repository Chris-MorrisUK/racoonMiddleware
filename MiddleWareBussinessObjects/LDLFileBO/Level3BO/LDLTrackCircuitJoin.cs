using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackCircuitJoin : LDLBOBase
    {
        public const string Identity = "TC_JOINT";
        public LDLTrackCircuit[] JoinedCircuits;
        public string[] JoinedCircuitStrs;
        public LDLMLocation Location;

        public LDLTrackCircuitJoin(string id)
            : base(id)
        {
            JoinedCircuits = new LDLTrackCircuit[2];
            JoinedCircuitStrs = new string[2];
        }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase foundTC = null;
            int idx = 0;
            foreach (string tcStr in JoinedCircuitStrs)
            {
                if (parsedObjects.TryGetValue(tcStr, out foundTC))
                    JoinedCircuits[idx++] = foundTC as LDLTrackCircuit;
            }
            if (this.Location != null)
                this.Location.DoSecondPass(parsedObjects);
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.TrackCircuitLocationUri;
            }
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.TrackCircuitLocation;
            }
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> customTripples = new List<BOTripple>();
            //I've been slightly arbitary assigning min and max here - assuming it's the end of the first and the start of the second
            if (JoinedCircuits[0] != null)
                customTripples.Add(BOTripple.CreateTrippleFromValues(JoinedCircuits[0].AsNode, LDLUris.MaxLocation, this.ObjectUri));
            if (JoinedCircuits[1] != null)
                customTripples.Add(BOTripple.CreateTrippleFromValues(JoinedCircuits[1].AsNode, LDLUris.MinLocation, this.ObjectUri));
            if (this.Location != null)
                customTripples.Add(new BOTripple(this.AsNode,new BONode( LDLUris.RelativePositionPropertyUri), Location.AsNode));
            customTripples.AddRange(Location.GetAsTripples());
            return customTripples;
        }
    }
}
