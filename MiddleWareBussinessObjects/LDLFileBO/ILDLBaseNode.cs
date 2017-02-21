using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public interface ILDLBaseNode
    {
        string Track1Str {get; set;}
        BONode AsNode { get; }
        
    }
}
