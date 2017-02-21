using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackEndFactory : LDLFactoryBase, IParsableFactory
    {
        private const string identifier = @"TRACK_END_NODE";

        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLTrackEndNode result = new LDLTrackEndNode(id);
            int definitionLineCount = definition.GetUpperBound(0);
            for (int idx = 0; idx <= definitionLineCount; idx++)
            {
                if (!ParseTrack1(definition[idx], result))
                    ParseGraphicPoint(definition[idx], result);

                if((!string.IsNullOrEmpty(result.Track1Str)) &&
                    (result.GraphicPoint != null))
                    break;
            }

            return result;
        }
    }
}
