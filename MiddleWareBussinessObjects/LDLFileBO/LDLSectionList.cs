using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSectionList : LDLBOBase
    {

        public static string Section = "M_SECTION";
        public const string SectionList = @"M_SECTION_LIST";
        public const string SectionListEndMark = @";";
        
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

        /// <summary>
        /// Not exactly a constructor - this extracts a section list from a part - parsed definition and returns the section list as an object        /// 
        /// </summary>
        /// <param name="definition">the definition of your object</param>
        /// <param name="nLines">The size, in lines, of the definition of the entire object, not just the section list</param>
        /// <param name="startIDX">how far through the definition to start looking - the last line on which you parsed something else</param>
        /// <returns>A LDLSectionList</returns>
        public static LDLSectionList ExtractSectionList(string[] definition, int nLines, int startIDX,bool isMultiPartSection = false)
        {
            List<string> sectionsDef = new List<string>();
            bool addToSections = false;
            string startMark = isMultiPartSection ? LDLSectionList.Section : LDLSectionList.SectionList;
            for (int idx = startIDX; idx < nLines; idx++)
            {
                if (definition[idx].Contains(startMark))
                    addToSections = true;
                if (addToSections)
                    sectionsDef.Add(definition[idx]);
                if ((addToSections)
                    && (definition[idx].Contains(LDLSectionList.SectionListEndMark)))
                    addToSections = false;
            }
            LDLSectionList sections = new LDLSectionList(sectionsDef);
            return sections;
        }

        public List<LDLMSection> Sections;
        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            foreach (LDLMSection Section in Sections)
                Section.DoSecondPass(parsedObjects);                
            
        }
    }
}
