using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLInterlockingFactory : LDLFactoryBase, IParsableFactory
    {
        
        private const string sectionList = @"M_SECTION_LIST";
        private const string sectionListEndMark = @";";
       
        public string Identifier
        {
            get { return LDLInterlocking.Identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLInterlocking result = new LDLInterlocking(id);
            int nLines = definition.GetLength(0);
            List<string> sectionsDef = new List<string>();
            
            bool addToSections = false;
            for (int idx = 0; idx < nLines; idx++)
            {
                if (definition[idx].Contains(sectionList))
                    addToSections = true;
                if (addToSections)                
                    sectionsDef.Add(definition[idx]);
                if ((addToSections)
                    && (definition[idx].Contains(sectionListEndMark)))
                    addToSections = false;

            }

            LDLSectionList sections = new LDLSectionList(sectionsDef);
            result.Sections = sections;

            return result;
        }
    }
}
