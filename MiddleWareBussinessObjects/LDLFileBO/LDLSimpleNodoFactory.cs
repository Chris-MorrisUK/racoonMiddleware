using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSimpleNodeFactory : LDLFactoryBase, IParsableFactory
    {
        private const string identifier = @"SIMPLE_NODE";
 
        private const string track_2_ident = @"TRACK_2";
        


        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLSimpleNode result = new LDLSimpleNode(id);
            int nLines = definition.GetUpperBound(0);
            for (int idx = 0; idx <= nLines; idx++)
            {
                if (!ParseTrack1(definition[idx],result))
                {
                    if (definition[idx].Contains(track_2_ident))
                        result.Track2Str = ParseItem(definition[idx]);
                    else ParseGraphicPoint(definition[idx], result);
                }

                if ((!string.IsNullOrEmpty(result.Track1Str))
                    && (!string.IsNullOrEmpty(result.Track2Str))
                    && (result.GraphicPoint != null))
                    break;//When all the fields that are done in this loop are done, break
            }

            return result;
        }

        

    }
}
