using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLBaliseGroup : LDLBOBase
    {
        public LDLBaliseGroup(string id):base(id)
        {
            BaliseGroupTypes = new List<LDLBaliseGroupType>();
            AssociatedWith = new List<string>();
            BaliseStrs = new List<string>();
            //CountryCode and BGID is actually parsed out of the ID string
            string[] idParts = id.Split(new char[]{'-'},StringSplitOptions.RemoveEmptyEntries);
            if (idParts.GetUpperBound(0) < 1)
                throw new InvalidOperationException("Balise Group ID format error");
            string cCodeStr = idParts[0].Substring(3,4);//it's a fixed length and offset
            CountryCode = UInt16.Parse(cCodeStr);
            int startBGID = idParts[1].IndexOf("_");//numbers starts with an underscore
            string bgGroupStr = idParts[1].Substring(startBGID + 1, idParts[1].Length - (startBGID + 1));
            BGID = UInt16.Parse(bgGroupStr);
            //CountryCode
        }
        public string RBCStr;
        public LDLRBC RBC { get; set; }
        public byte ERTMSLevel { get; set; }//4 = STM
        public List<LDLBaliseGroupType> BaliseGroupTypes;
        public List<LDLBalise> Balises;
        public List<string> BaliseStrs;
        public List<string> AssociatedWith;
        public UInt16 CountryCode;
        public UInt16 BGID;

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {

            List<BOTripple> customTriples = new List<BOTripple>();
            foreach (LDLBalise balise in Balises)
                customTriples.Add(new BOTripple(this.AsNode, new BONode(LDLUris.RDFSMemberPropertyUri), balise.AsNode));
            Uri BGTypeUri = null;
            foreach (LDLBaliseGroupType bgGroupType in BaliseGroupTypes)
            {
                switch (bgGroupType)
                {
                    case LDLBaliseGroupType.SIGNAL:
                        BGTypeUri = LDLUris.BGTypeSignalUri;
                        break;
                    case LDLBaliseGroupType.ERTMS_LEVEL_TRANSITION:
                        BGTypeUri = LDLUris.BGTypeERTMSLevelTransUri;
                        break;
                    case LDLBaliseGroupType.LT_ANNOUNCEMENT:
                        BGTypeUri = LDLUris.BGTypeLTAnnouceUri;
                        break;
                    case LDLBaliseGroupType.IN_FILL:
                        BGTypeUri = LDLUris.BGTypeInFillUri;
                        break;
                    case LDLBaliseGroupType.MAIN:
                        BGTypeUri = LDLUris.BGTypeMainUri;
                        break;
                    case LDLBaliseGroupType.LEVEL_CROSSING:
                        BGTypeUri = LDLUris.BGTypeLevelCrossingUri;
                        break;
                    case LDLBaliseGroupType.RBC_BOUNDARY:
                        BGTypeUri = LDLUris.BGTypeRBC_BoundaryUri;
                        break;
                    case LDLBaliseGroupType.ODOMETRY:
                        BGTypeUri = LDLUris.BGTypeOdometrynUri;
                        break;
                    default:
                        break;
                }
                BOTripple bgTypeTriple = BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.BGTypePropertyUri, BGTypeUri);
                customTriples.Add(bgTypeTriple);
            }
            
            return customTriples;//CC_0002-BG_20

        }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase foundObject = null;
            if (!string.IsNullOrEmpty(RBCStr))
            {                
                if (parsedObjects.TryGetValue(RBCStr, out foundObject))
                    RBC = foundObject as LDLRBC;
            }
            if (BaliseStrs.Count > 0)
            {
                Balises = new List<LDLBalise>(); 
                foreach (string baliseID in BaliseStrs)
                {
                    if (parsedObjects.TryGetValue(baliseID, out foundObject))
                        Balises.Add((LDLBalise)foundObject);
                }
            }

        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.BaliseGroupTypeStr;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.BaliseGroupTypeUri;
            }
        }
    }
}
