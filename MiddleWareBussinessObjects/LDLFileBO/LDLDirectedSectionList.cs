using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLDirectedSectionList: LDLBOBase
    {
        public const string Identitfier = "D_M_SECTION";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="def">The definition of some bussiness object, which includes a directed section list</param>
        /// <param name="startLine">The line on which the Directed section list is defined</param>
        /// <param name="upperBound">The number of lines in the definition</param>
        public LDLDirectedSectionList(string[] def,int startLine,int upperBound) : base(string.Empty) 
        {
            Sections = new List<LDLDirectedSection>();

            for (int lineN = startLine; lineN <= upperBound; lineN++)
            {
                int sectionStartIdx = def[lineN].IndexOf(LDLSeperators.SUB_SECTION_START);
                int sectionEndIdx = def[lineN].LastIndexOf(LDLSeperators.SUB_SECTION_END);
                LDLDirectedSection toAdd = new LDLDirectedSection(def[lineN].Substring(sectionStartIdx + 1, (sectionEndIdx - sectionStartIdx) - 1));
                Sections.Add(toAdd);

                if (def[lineN].Contains(LDLSectionList.SectionListEndMark))
                    break;
            }
        }

        public List<LDLDirectedSection> Sections;
    }
}
