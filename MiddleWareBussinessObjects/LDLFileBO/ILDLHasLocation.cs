using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public interface ILDLHasGraphicLocation
    {
        LDLPoint GraphicPoint { get; set; }
    }
}
