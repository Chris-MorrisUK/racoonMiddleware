using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLInterlocking : LDLBOBase
    {
        private const string identifier = @"INTERLOCKING";

        public static string Identifier
        {
            get
            {
                return identifier;
            }
        }
        public LDLInterlocking(string id)
            :base(id)
        {
            Sections = new LDLSectionList();
        }
        public LDLSectionList Sections;

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            Sections.DoSecondPass(parsedObjects);
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.InterlockingStr;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.InterlockingURI;
            }
        }
    }
}
