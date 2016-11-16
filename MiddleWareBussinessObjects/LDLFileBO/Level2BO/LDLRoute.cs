using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLRoute : LDLBOBase 
    {

        public const string TypeID = "ROUTE";

        public LDLRoute(string id)
            : base(id)
        {
            RouteConfirmResponses = new List<string>();
        }
        public LDLRouteType RouteType;
        public LDLInterlocking Interlocking;
        public string InterlockingStr;
        public LDLSignal Entrance;
        public LDLIHasDirectedLocation Exit;
        public string EntranceStr;
        public string ExitStr;
        public string Overlap_ID;
        //Not in the sample data
        /*[ALLOCATED_TO_ARS" ":" boolean ";" ]
         [ "ROUTE_PROVING_EXEMPTION" ":" boolean ";" 
         ARS_SUB_AREAS
         RING
         TCS_IN_ROUTE
         * TCS_IN_OVERLAP
         * TERMINAL_STATION_ROUTE_AVAILABLE
         * MAX_WAIT_TIME
         * DELAY_RULE         * 
         */
        public int Length;//not sure this is in the data but easy to include;
        public string PanelRequest;
        public string PanelCancelRequest;
        public List<string> RouteConfirmResponses;
        public LDLDirectedSectionList Sections;

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase foundObject;
            if (!string.IsNullOrEmpty(InterlockingStr))
            {
                parsedObjects.TryGetValue(InterlockingStr, out foundObject);
                Interlocking = foundObject as LDLInterlocking;
            }
            if (!string.IsNullOrEmpty(EntranceStr))
            {
                parsedObjects.TryGetValue(EntranceStr, out foundObject);
                Entrance = foundObject as LDLSignal;
            }
            if (!string.IsNullOrEmpty(ExitStr))
            {
                parsedObjects.TryGetValue(ExitStr, out foundObject);
                Exit = foundObject as LDLIHasDirectedLocation;
            }
            if (Sections != null)
                Sections.DoSecondPass(parsedObjects);
           
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.RouteUri;
            }
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.RouteStr;
            }
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> customTripples = new List<BOTripple>();
            
            return customTripples;
        }
    }
}
