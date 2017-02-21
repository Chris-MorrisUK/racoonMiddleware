using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSwitchAndDimondFactoryWorker: LDLFactoryBase
    {
        private const string normalPath = @"NORMAL_PATH";
        private const  string reversePath = @"REVERSE_PATH";
        private const string typeStr = "TYPE";

        protected  static void PopulateItemText(string[] definition, string id, LDLSandCBASE toPopulate)
        {            
            int definitionLineCount = definition.GetUpperBound(0);
            LDLPointsNode asPoints = toPopulate as LDLPointsNode;

            for (int idx = 0; idx <= definitionLineCount; idx++)
            {                
                if (!ParseGraphicPoint(definition[idx], toPopulate))
                {
                    if (definition[idx].Contains(normalPath))
                        toPopulate.NormalPath = new LDLTrackPair(definition[idx]);
                    else if (definition[idx].Contains(reversePath))
                        toPopulate.ReversePath = new LDLTrackPair(definition[idx]);
                    if (asPoints != null)
                    {
                        if (definition[idx].Contains(typeStr))
                            asPoints.PNType = ParseItem(definition[idx]);
                    }
                }

                if ( (toPopulate.NormalPath != null) &&
                    (toPopulate.GraphicPoint != null) &&
                    (toPopulate.ReversePath != null)                    
                    )
                {                    
                    if (asPoints != null)
                        if (!string.IsNullOrEmpty(asPoints.PNType))
                            break;
                }
                   
                    
            }
        }
    }
}
