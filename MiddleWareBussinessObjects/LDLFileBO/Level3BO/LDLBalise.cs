using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLBalise : LDLBOBase
    {
        public LDLBalise(string id):base(id)
        { 

        }
        public bool FixedData { get; set; }
        public BaliseDuplicateStatus DuplicateType;
        public LDLMLocation Location {get; set;}
        //q_rbc is unused

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            if (Location != null)
                Location.DoSecondPass(parsedObjects);
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.BaliseTypeStr;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.BaliseTypeUri;
            }
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> customTripples = new List<BOTripple>();
            Uri duplicateTypeNodeUri = null;
            switch (DuplicateType)
            {
                case BaliseDuplicateStatus.NO_DUPLICATES:
                    duplicateTypeNodeUri = LDLUris.BaliseNoDuplicateClassUri;
                    break;
                case BaliseDuplicateStatus.DUPLICATE_OF_THE_NEXT_BALISE:
                    duplicateTypeNodeUri = LDLUris.BaliseDuplicateNextClassUri;
                    break;
                case BaliseDuplicateStatus.DUPLICATE_OF_THE_PREVIOUS_BALISE:
                    duplicateTypeNodeUri = LDLUris.BaliseDuplicatePrevousClassUri;
                    break;
                case BaliseDuplicateStatus.SPARE:
                    duplicateTypeNodeUri = LDLUris.BaliseDuplicateSpareClassUri;
                    break;
            }
            customTripples.Add(BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.BaliseDuplicatePropertyUri, duplicateTypeNodeUri));
            customTripples.Add(BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.BaliseFixedProperty, FixedData));
            if (this.Location != null)
                customTripples.Add(new BOTripple(this.AsNode, new BONode(LDLUris.RelativePositionPropertyUri), Location.AsNode));
            customTripples.AddRange(Location.GetAsTripples());
            return customTripples;
        }

    }
}
