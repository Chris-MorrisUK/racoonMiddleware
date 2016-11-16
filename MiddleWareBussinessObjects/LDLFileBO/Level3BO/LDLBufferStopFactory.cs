using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLBufferStopFactory: LDLFactoryBase, IParsableFactory
    {
        private string ident = "BUFFER_STOP";
        public string Identifier
        {
            get { return ident; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLBufferStop bufferStop = new LDLBufferStop(id);
            bufferStop.Location= new LDLDirectedLocation(definition[0]);
            return bufferStop;
        }
    }
}
