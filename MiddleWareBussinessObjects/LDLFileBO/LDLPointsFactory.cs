using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLPointsFactory : LDLSwitchAndDimondFactoryWorker, IParsableFactory
    {
        private string identifier = "POINTS_NODE";

        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLPointsNode result = new LDLPointsNode(id);
            LDLSwitchAndDimondFactoryWorker.PopulateItemText(definition, id, result);
            return result;
        }
    }
}
