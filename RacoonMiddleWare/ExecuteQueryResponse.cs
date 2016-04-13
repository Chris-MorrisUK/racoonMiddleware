using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonMiddleWare
{
    [DataContract]
    public class ExecuteQueryResponse : SimpleRacoonResponse, IResponseWithOutput
    {
        [DataMember]
        public List<StringParameter> OutputParameters;

        #region IResponseWithOutput Members

        public void SetOutputParameters(IEnumerable<ParameterBase> _outputParameters)
        {
            OutputParameters = new List<StringParameter>();
            foreach (ParameterBase baseParam in _outputParameters)
                OutputParameters.Add((StringParameter)baseParam);
        }

        #endregion
    }
}