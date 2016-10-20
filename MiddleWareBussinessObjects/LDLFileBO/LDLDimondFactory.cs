using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLDimondFactory : LDLFactoryBase, IParsableFactory
    {
        private const string path1 = "PATH_1";
        private const string path2 = "PATH_2";

        private const string identifier = "DIAMOND_NODE";
        
        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLDimondNode toPopulate = new LDLDimondNode(id);
            int definitionLineCount = definition.GetUpperBound(0);
        
            for (int idx = 0; idx < definitionLineCount; idx++)
            {
                if (!ParseGraphicPoint(definition[idx], toPopulate))
                {
                    if (definition[idx].Contains(path1))
                        toPopulate.Path1 = new LDLTrackPair(definition[idx]);
                    else if (definition[idx].Contains(path2))
                        toPopulate.Path2 = new LDLTrackPair(definition[idx]);                    
                }

                if ((toPopulate.Path1 != null) &&
                    (toPopulate.GraphicPoint != null) &&
                    (toPopulate.Path2 != null)
                    )
                        break;
                


            }

            return toPopulate;
        }
    }
}
