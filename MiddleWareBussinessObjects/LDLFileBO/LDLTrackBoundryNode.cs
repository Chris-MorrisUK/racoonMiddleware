using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackBoundryNode : LDLTrackEndNode,  ILDLBaseNode, ILDLHasGraphicLocation
    {
        public string ConnectedArea;
        public string Label;

        public LDLTrackBoundryNode(string id) :base (id)
        { }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> results = new List<BOTripple>();
            results.AddRange(base.GetAsTripples());
            if(!string.IsNullOrEmpty(Label))
                results.Add(BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.Label, Label));
            return results;
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.RouteBoundary;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.RouteBoundaryUri;
            }
        }

    }
}
