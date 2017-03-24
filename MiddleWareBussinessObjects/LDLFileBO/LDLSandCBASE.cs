using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSandCBASE : LDLBOBase, ILDLHasGraphicLocation, ILDLBaseNode
    {
        public LDLTrackPair NormalPath;
        public LDLTrackPair ReversePath;
        public LDLPoint GraphicPoint { get; set; }

        public LDLSandCBASE(string id)
            : base(id)
        { }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            if(NormalPath != null)
                NormalPath.DoSecondPass(parsedObjects);
            if(ReversePath != null)
            ReversePath.DoSecondPass(parsedObjects);
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> results = new List<BOTripple>();
            if (GraphicPoint != null)
                results.AddRange(GraphicPoint.GetAsTripples(this.AsNode));
            if (NormalPath != null)
            {
                BONode startNodeObject = NormalPath.Track1.AsNode;
                BOTripple startNodeTripple = new BOTripple(this.AsNode, new BONode(LDLUris.StartArcProperty), startNodeObject);
                results.Add(startNodeTripple);
            }
        
            BONode normalEndNode = this.NormalPath.Track2.AsNode;
            BOTripple normalEndNodeTripple = new BOTripple(this.AsNode, new BONode(LDLUris.EndArcProperty), normalEndNode);
            results.Add(normalEndNodeTripple);
            BONode alternativeEndNode = this.ReversePath.Track2.AsNode;
            BOTripple altEndNodeTripple = new BOTripple(this.AsNode, new BONode(LDLUris.EndArcProperty), alternativeEndNode);
            results.Add(altEndNodeTripple);
            return results;
        }

        public string Track1Str
        {
            get
            {
                if (NormalPath != null)
                    return NormalPath.Track1Str;
                else
                    return string.Empty;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }
    }
}
