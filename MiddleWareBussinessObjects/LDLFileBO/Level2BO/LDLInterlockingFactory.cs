using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLInterlockingFactory : LDLFactoryBase, IParsableFactory
    {
        

       
        public string Identifier
        {
            get { return LDLInterlocking.Identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLInterlocking result = new LDLInterlocking(id);
            int nLines = definition.GetLength(0);
            LDLSectionList sections = LDLSectionList.ExtractSectionList(definition, nLines, 0);
            result.Sections = sections;
            return result;
        }

        
    }
}
