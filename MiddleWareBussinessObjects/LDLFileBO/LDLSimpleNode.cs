using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSimpleNode : LDLBOBase,  ILDLBaseNode, ILDLHasGraphicLocation
    {
        public string Track1Str { get; set; }
        public string Track2Str;

        public LDLTrack Track_1;
        public LDLTrack Track_2;

        public LDLPoint GraphicPoint { get; set; }

        public LDLSimpleNode(string id)
            : base(id)
        { }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase trackObject;
            parsedObjects.TryGetValue(Track1Str, out trackObject);
            Track_1 = trackObject as LDLTrack;
            parsedObjects.TryGetValue(Track2Str, out trackObject);
            Track_2 = trackObject as LDLTrack;
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.RouteNodeStr;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.RouteNodeUri;
            }
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> results = new List<BOTripple>();
            if (this.GraphicPoint != null)
                results.AddRange(GraphicPoint.GetAsTripples(this.AsNode));
            if (this.Track_1 != null)
            {
                BOTripple track1Triple = new BOTripple(this.AsNode, new BONode(LDLUris.arcProperty), this.Track_1.AsNode);
                results.Add(track1Triple);
            }
            if (this.Track_2 != null)
            {
                BOTripple track2Triple = new BOTripple(this.AsNode, new BONode(LDLUris.arcProperty), this.Track_2.AsNode);
                results.Add(track2Triple);
            }
            return results;
        }

        
    }
}
