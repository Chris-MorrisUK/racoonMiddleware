using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackEndNode : LDLBOBase,  ILDLBaseNode, ILDLHasGraphicLocation
    {
        public LDLTrackEndNode(string id)
            : base(id)
        { }
        public string Track1Str { get; set; }


        public LDLTrack Track_1;
        public LDLPoint GraphicPoint { get; set; }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase trackObject;
            parsedObjects.TryGetValue(Track1Str, out trackObject);
            Track_1 = trackObject as LDLTrack;
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.Terminus;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return  LDLUris.TerminusURI;
            }
        }

        protected override  IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> results = new List<BOTripple>();
            if (this.GraphicPoint != null)
                results.AddRange(GraphicPoint.GetAsTripples(this.AsNode));
            if (this.Track_1 != null)
            {
                BOTripple track1Triple = new BOTripple(this.AsNode, new BONode(LDLUris.arcProperty), this.Track_1.AsNode);
                results.Add(track1Triple);
            }
            return results;
        }
    }
}
