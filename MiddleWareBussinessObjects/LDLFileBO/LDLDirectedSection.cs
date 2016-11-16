using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLDirectedSection : LDLMSection
    {
        public ILDLBaseNode Towards;
        private string towardsStr;
        private const char SpeachMark = '\"';

        public LDLDirectedSection(string definition)            
        { 
            int startBaseDef = definition.LastIndexOf(LDLSeperators.SUB_SECTION_START)+3;//Lose the open bracket and the open speech mark
            int endBase = definition.IndexOf(LDLSeperators.SUB_SECTION_END) ;
            int lengthBase = (endBase- startBaseDef)-1;
            string baseDef = definition.Substring(startBaseDef, lengthBase);
            base.Populate(baseDef);
            int startNodeDef = definition.IndexOf(SpeachMark, endBase)+1;//It's probably also endBase + 3 but better safe than sorry
            int endNodeDef = definition.IndexOf(SpeachMark, startNodeDef);
            towardsStr = definition.Substring(startNodeDef, (endNodeDef - startNodeDef));
        }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            if (!string.IsNullOrEmpty(towardsStr))
            {
                LDLBOBase asBase = null;
                if (parsedObjects.TryGetValue(towardsStr, out asBase))
                    Towards = asBase as ILDLBaseNode;
                    
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.RelativePositionNounUri;
            }
        }
        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.RelativePositionNoun;
            }
        }


    }
}
