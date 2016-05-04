using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonMiddleWare
{
	[DataContract]
	public class MultiVariableResponse: SimpleRacoonResponse , IResponseWithOutput
	{
		[DataMember]
		public List<MultiParameterResult> OutputParameters;

		public void SetOutputParameters(IEnumerable<ParameterBase> _outputParameters)
		{
			OutputParameters = new List<MultiParameterResult>();
			foreach (ParameterBase baseParam in _outputParameters)
				OutputParameters.Add((MultiParameterResult)baseParam);
		}
	}
}