using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLGraphicLine
    {

        public LDLGraphicLine()
        {
            Points = new List<LDLPoint>();
        }

        

        public List<LDLPoint> Points { get; private set; }
    }
}
