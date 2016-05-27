using RacoonMiddleWare;
using RacoonServices;
using System.Collections.Generic;
using System.Runtime.Serialization;

[DataContract]
public class ExecuteQueryBytesResponse : SimpleRacoonResponse, IResponseWithOutput
{

    [DataMember]
    public List<ByteParameter> OutputParameters;



    #region IResponseWithOutput Members

    public void SetOutputParameters(IEnumerable<ParameterBase> _outputParameters)
    {
        OutputParameters = new List<ByteParameter>();
        foreach (ParameterBase baseParam in _outputParameters)
            OutputParameters.Add((ByteParameter)baseParam);
    }

    #endregion

    
}