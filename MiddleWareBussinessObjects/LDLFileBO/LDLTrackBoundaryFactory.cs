using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackBoundaryFactory : LDLFactoryBase, IParsableFactory
    {
        private const string identifier = @"TRACK_BOUNDARY_NODE";
        private const string connectedArea = @"CONNECTED_AREA";
        private const string label = @"LABEL";

        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLTrackBoundryNode result = new LDLTrackBoundryNode(id);
            int definitionLineCount = definition.GetUpperBound(0);
            for (int idx = 0; idx <= definitionLineCount; idx++)
            {
                if (!ParseTrack1(definition[idx], result))
                    if(!ParseGraphicPoint(definition[idx], result))                        
                {
                    if (definition[idx].Contains(connectedArea))
                        result.ConnectedArea = ParseItem(definition[idx]);
                    else if (definition[idx].Contains(label))
                        result.Label = ParseItem(definition[idx]);
                }


                if ((!string.IsNullOrEmpty(result.Track1Str)) &&
                    (result.GraphicPoint != null) &&
                    ((!string.IsNullOrEmpty(result.ConnectedArea))) &&
                    ((!string.IsNullOrEmpty(result.Label))) 
                    )
                    break;
            }

            return result;
        }
    }
}
