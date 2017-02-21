using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLDirectedLocation : LDLMLocation
    {
        public const string DirectedLocationIdentifier = @"D_M_LOCATION";
        public readonly string NodeStr;
        public ILDLBaseNode Node;

        public LDLDirectedLocation(string def)
        {
            int lengthDef = def.Length;
            int startNode = -1;
            int endNode = -1;
            for (int i = lengthDef-1; i > 0; i--)
            {
                if (def[i] == LDLSeperators.SUB_SECTION_END)
                    endNode = i;
                else if (def[i] == LDLSeperators.MEASUREMENT_SEPERATOR)
                {
                    startNode = i;
                    break; // we've found the two things we need
                }
            }
            NodeStr = def.Substring(startNode + 3, (endNode - startNode) - 5).Trim();
            int startLocation = def.IndexOf(LDLSeperators.SUB_SECTION_START) + 1;
            string mLocationStr = def.Substring(startLocation, startNode  - startLocation);
            PopulateLocationFields(mLocationStr);
        }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase nodeObject;
            parsedObjects.TryGetValue(NodeStr, out nodeObject);
            Node = nodeObject as ILDLBaseNode;
            base.DoSecondPass(parsedObjects);
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> customTripples = new List<BOTripple>( base.GetCustomTripples());
            customTripples.Add(new BOTripple(this.AsNode, new BONode(LDLUris.EndNodeProperty), Node.AsNode));//Not sure this is a beautiful way of modeling it, but it's what is in the file
            return customTripples;
        }
    }
}
