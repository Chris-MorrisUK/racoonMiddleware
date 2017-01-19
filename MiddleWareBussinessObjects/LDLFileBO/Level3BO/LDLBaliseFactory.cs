using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLBaliseFactory : LDLFactoryBase, IParsableFactory
    {
        private const string baliseType = "TYPE";
        private const string isDuplicated = "IS_DUPLICATED";
        private string ident = "BALISE";
        public string Identifier
        {
            get { return ident; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLBalise balise = new LDLBalise(id);
            int nLines = definition.GetUpperBound(0);
            for (int idx = 0; idx <= nLines; idx++)
            {
                if(definition[idx].Contains(baliseType))
                {
                    string bType = ParseItem(definition[idx]);
                    if (string.Compare("FIXED_DATA", bType) == 0)
                        balise.FixedData = true;
                    else
                        balise.FixedData = false;
                }
                else if (definition[idx].Contains(isDuplicated))
                {
                    balise.DuplicateType = (BaliseDuplicateStatus)Enum.Parse(typeof(BaliseDuplicateStatus), ParseItem(definition[idx]));
                }
                else if (definition[idx].Contains(LDLMLocation.Identifier))
                    balise.Location = LDLMLocation.CreateFromCompleteLine(definition[idx]);
            }
            return balise;
        }
    }
}
