using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSeperators
    {
        public const string SECTION_START = @"@@@";
        public const string SECTION_END = @"@@@@";
        public const string CLASS_ID_DIVIDE = @"::";
        public const string LINE_SEPERATOR = @":";
        public const string TERMINATER = @";";
        public const char SUB_SECTION_START = '(';
        public const char SUB_SECTION_END = ')';
        public const char MEASUREMENT_SEPERATOR = ',';
        public const char ABSOLUTE_DATA_COL_SEPERATOR = '\t';
    }
}
