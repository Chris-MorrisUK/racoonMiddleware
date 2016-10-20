using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSectionList : LDLBOBase
    {
        
        public LDLSectionList():base(string.Empty)
        {
            Sections = new List<LDLMSection>();
        }

        public LDLSectionList(List<string> definition)
            : this()
        {
            foreach (string line in definition)
            {
                int sectionStartIdx = line.IndexOf(LDLSeperators.SUB_SECTION_START);
                int sectionEndIdx = line.IndexOf(LDLSeperators.SUB_SECTION_END, sectionStartIdx);
                LDLMSection toAdd = new LDLMSection(line.Substring(sectionStartIdx + 1, (sectionEndIdx - sectionStartIdx)-1));
                Sections.Add(toAdd);
            }
        }

        public List<LDLMSection> Sections;
        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            foreach (LDLMSection Section in Sections)
                Section.DoSecondPass(parsedObjects);                
            
        }
    }
}
