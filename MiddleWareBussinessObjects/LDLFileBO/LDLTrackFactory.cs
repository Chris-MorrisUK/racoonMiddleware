using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackFactory :LDLFactoryBase, IParsableFactory
    {
        private const string identifier = @"TRACK";
        private const string node_0 = @"NODE_0";
        private const string node_1 = @"NODE_1";
        private const string LENGTH = "LENGTH";

        

        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLTrack result = new LDLTrack(id);
            int graphicLineStartIndex = -1;
            int index =0;
            int nLines = definition.GetLength(0);

            foreach (string line in definition)//Don't be clever with continues in this loop - it messes up index
            {
                if (line.Contains(node_0))
                {
                    result.Node0AsStr = ParseItem(line);
                    
                }else  if (line.Contains(node_1))
                {
                    result.Node1AsStr = ParseItem(line);
                   
                }else  if (line.Contains(graphic_line))
                {
                    graphicLineStartIndex = index;

                }
                else if (line.Contains(LENGTH))
                {
                    result.Length = decimal.Parse(ParseItem(line));
                }

                index++;

                if ((!string.IsNullOrEmpty(result.Node1AsStr))
                    && (!string.IsNullOrEmpty(result.Node0AsStr))
                    && (result.Length > 0)
                    && (graphicLineStartIndex > 0))
                        break;//When all the fields that are done in this loop are done, break
            }

            if (graphicLineStartIndex > 0)
                CreateGraphicLine(definition, result, graphicLineStartIndex, nLines);

            return result;
        }




        

 

    }
}
