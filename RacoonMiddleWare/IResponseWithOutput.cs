using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacoonMiddleWare
{
    public interface IResponseWithOutput : IRacoonResponse
    {
        void SetOutputParameters(IEnumerable<ParameterBase> _outputParameters);
    }
}
