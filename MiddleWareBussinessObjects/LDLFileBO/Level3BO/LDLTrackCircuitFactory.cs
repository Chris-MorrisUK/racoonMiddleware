using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackCircuitFactory : LDLFactoryBase, IParsableFactory
    {
        private const string typedefStr = "TYPE";

         public string Identifier
        {
            get { return LDLTrackCircuit.Identitifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLTrackCircuit toPopulate = new LDLTrackCircuit(id);
            int nLines = definition.GetUpperBound(0);
            int startOfSections = 0;
            for (int i = 0; i < nLines; i++)
            {
                if (string.IsNullOrEmpty(toPopulate.InterlockingStr))
                    if (definition[i].Contains(LDLInterlocking.Identifier))
                        toPopulate.InterlockingStr = ParseItem(definition[i]);
                
                if (definition[i].Contains(typedefStr))
                {
                    string typeStr = ParseItem(definition[i]);
                    if (!string.IsNullOrEmpty(typeStr))
                        toPopulate.TCType = (LDLTrackCircuitTypeEnum)Enum.Parse(typeof(LDLTrackCircuitTypeEnum), typeStr);
                    startOfSections = i;
                    break;
                }
                
            }
            toPopulate.Sections = LDLSectionList.ExtractSectionList(definition, nLines+1, startOfSections,true);

            return toPopulate;
        }
    }
}
