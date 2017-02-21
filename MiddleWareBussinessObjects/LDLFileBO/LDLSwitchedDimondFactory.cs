using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSwitchedDimondFactory : LDLSwitchAndDimondFactoryWorker, IParsableFactory
    {
        private string identifier = "SWITCHED_DIAMOND_NODE";

        public string Identifier
        {
            get { return identifier; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLSwitchedDimondNode result = new LDLSwitchedDimondNode(id);
            LDLSwitchAndDimondFactoryWorker.PopulateItemText(definition, id, result);
            return result;
        }
    }
}
