using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLBaliseGroupFactory : LDLFactoryBase, IParsableFactory
    {
        private static string RBCIdent = "RBC";
        private static string ERTMSLevel = "ERTMS_LEVEL";        
        private static string bgTypeList = "TYPE_LIST";
        private static string baliseList = "BALISE_LIST";
        private static string associatedWith = "ASSOCIATED_WITH";

        private static string identifier = "BALISE_GROUP";
        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
             LDLBaliseGroup baliseGroup = new LDLBaliseGroup(id);
            int nLines = definition.GetUpperBound(0);
            listStateEnum listState = listStateEnum.OtherData;
            for (int idx = 0; idx <= nLines; idx++)
            {
                string trimmedOnly = definition[idx].Trim();
                string parsed = ParseItem(trimmedOnly);               

                switch (listState)
                {
                    case listStateEnum.OtherData:
                        //This is the normal, default, condition in which the code should fall through and process the line as normal
                        break;
                        /*All the bellow suggest that the current line is part of a multi-line list*/
                    case listStateEnum.inAssWith:
                        baliseGroup.AssociatedWith.Add(parsed);
                        break;
                    case listStateEnum.inBaliseList:
                        baliseGroup.BaliseStrs.Add(parsed);
                        break;
                    case listStateEnum.inTypeList:
                        addBgType(baliseGroup, parsed);
                        break;                    
                }

                if ((listState != listStateEnum.OtherData) &&
                    (trimmedOnly.EndsWith(LDLSeperators.TERMINATER)))
                {
                    listState = listStateEnum.OtherData;
                    continue;
                }


                if ((baliseGroup.RBCStr == null) && (definition[idx].Contains(RBCIdent)))
                {
                    baliseGroup.RBCStr = parsed;
                    continue;
                }
                if (trimmedOnly.StartsWith(ERTMSLevel))
                {
                    string ertmsLevelStr = parsed;
                    if (char.IsDigit(ertmsLevelStr[0]))
                        baliseGroup.ERTMSLevel = byte.Parse(ertmsLevelStr);
                    else
                        baliseGroup.ERTMSLevel = 4;//STM = 4
                    continue;
                }
                if (trimmedOnly.StartsWith(bgTypeList))
                {
                    if (trimmedOnly.EndsWith(LDLSeperators.MEASUREMENT_SEPERATOR.ToString()))
                        listState = listStateEnum.inTypeList;
                    addBgType(baliseGroup, parsed);
                    continue;
                }
                if (trimmedOnly.StartsWith(baliseList))
                {

                    if (trimmedOnly.EndsWith(LDLSeperators.MEASUREMENT_SEPERATOR.ToString()))
                        listState = listStateEnum.inBaliseList;
                    baliseGroup.BaliseStrs.Add(parsed);
                    continue;
                }
                if (trimmedOnly.StartsWith(associatedWith))
                {
                    baliseGroup.AssociatedWith.Add(parsed);
                    if (trimmedOnly.EndsWith(LDLSeperators.MEASUREMENT_SEPERATOR.ToString()))
                        listState = listStateEnum.inAssWith;                  
                }
                
            }
            return baliseGroup;
        }

        private static void addBgType(LDLBaliseGroup baliseGroup, string parsed)
        {
            baliseGroup.BaliseGroupTypes.Add((LDLBaliseGroupType)Enum.Parse(typeof(LDLBaliseGroupType), parsed));
        }

        private enum listStateEnum
        {
            OtherData, 
            inAssWith, 
            inBaliseList, 
            inTypeList
        }
    }
}
