using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLBufferStop:LDLBOBase, LDLIHasDirectedLocation
    {
        public LDLBufferStop(string id)
            : base(id)
        { }
        public LDLDirectedLocation Location { get; set; }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            Location.DoSecondPass(parsedObjects);
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.BufferStopStr;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.BufferStop;
            }
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            return base.GetCustomTripples();
        }
    }
}
