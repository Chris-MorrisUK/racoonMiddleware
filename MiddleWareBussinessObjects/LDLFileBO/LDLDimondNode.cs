using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLDimondNode: LDLBOBase,  ILDLHasGraphicLocation
    {

        public LDLTrackPair Path1;
        public LDLTrackPair Path2;
        

        public LDLDimondNode(string id)
            : base(id)
        { }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            if (Path1 != null)
                Path1.DoSecondPass(parsedObjects);
            if (Path2 != null)
                Path2.DoSecondPass(parsedObjects);
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.Crossing;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.CorssingUri;
            }
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> customTripples = new List<BOTripple>();

            BOTripple Path1ATripple = new BOTripple();
            Path1ATripple.Subject = this.AsNode;
            Path1ATripple.Predicate = new BONode(LDLUris.arcProperty);
            Path1ATripple.Object = this.Path1.Track1.AsNode;

            BOTripple Path1BTripple = new BOTripple();
            Path1BTripple.Subject = this.AsNode;
            Path1BTripple.Predicate = new BONode(LDLUris.arcProperty);
            Path1BTripple.Object = this.Path1.Track2.AsNode;

            BOTripple Path2ATripple = new BOTripple();
            Path2ATripple.Subject = this.AsNode;
            Path2ATripple.Predicate = new BONode(LDLUris.arcProperty);
            Path2ATripple.Object = this.Path2.Track1.AsNode;

            BOTripple Path2BTripple = new BOTripple();
            Path2BTripple.Subject = this.AsNode;
            Path2BTripple.Predicate = new BONode(LDLUris.arcProperty);
            Path2BTripple.Object = this.Path2.Track2.AsNode;

            customTripples.Add(Path1ATripple);
            customTripples.Add(Path1BTripple);
            customTripples.Add(Path2ATripple);
            customTripples.Add(Path2BTripple);

            return customTripples;

        }
        public LDLPoint GraphicPoint {get; set;}
    }
}
