using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLRouteFactory : LDLFactoryBase, IParsableFactory
    {

        private const string typeStr = "TYPE";
        private const string entrance = "ENTRANCE";
        private const string exit = "EXIT";
        private const string panelRequest = "PANEL_REQUEST";
        private const string panelCancel = "PANEL_CANCEL_REQUEST";
        private const string routeConfirm = "ROUTE_CONFIRM_RESPONSE";


        public string Identifier
        {
            get { return LDLRoute.TypeID; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            int nLines = definition.GetUpperBound(0);
            if (nLines == 0) return null;
            LDLRoute route = new LDLRoute(id);

            for (int idx = 0; idx <= nLines; idx++)
            {
                if (definition[idx].Contains(typeStr))
                {
                    route.RouteType = (LDLRouteType)Enum.Parse(typeof(LDLRouteType), ParseItem(definition[idx]));
                    continue;
                }
                if ((string.IsNullOrEmpty(route.InterlockingStr)&& (definition[idx].Contains(LDLInterlocking.Identifier))))
                {
                    route.InterlockingStr = ParseItem(definition[idx]);
                    continue;
                }
                if((string.IsNullOrEmpty(route.EntranceStr))&&(definition[idx].Contains(entrance)))
                {
                    route.EntranceStr = ParseItem(definition[idx]);
                    continue;
                }
                if((string.IsNullOrEmpty(route.ExitStr))&&(definition[idx].Contains(exit)))
                {
                    route.ExitStr = ParseItem(definition[idx]);
                    continue;
                }
                if((string.IsNullOrEmpty(route.PanelRequest))&&(definition[idx].Contains(panelRequest)))
                {
                    route.PanelRequest = ParseItem(definition[idx]);
                    continue;
                }
                if((string.IsNullOrEmpty(route.PanelCancelRequest))&&(definition[idx].Contains(panelCancel)))
                {
                    route.PanelCancelRequest = ParseItem(definition[idx]);
                    continue;
                }
                if (definition[idx].Contains(routeConfirm))//You can have multiple panel confirm strings, though they may  e comma seperated
                {
                    route.RouteConfirmResponses.Add(ParseItem(definition[idx]));
                    continue;
                }
                 if (definition[idx].Contains(LDLDirectedSectionList.Identitfier))
                 {
                     route.Sections = new LDLDirectedSectionList(definition,idx,nLines);
                     break;//This is the last item defined
                 }
            }

            return route;

        }
    }
}
