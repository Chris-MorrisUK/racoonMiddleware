using MiddleWareBussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonMiddleWare
{
	 [DataContract(IsReference = true)]
	public class MultiParameterResult : ParameterBase, IConvertToMiddlewareParam
	{
		 public MultiParameterResult()
		 {
			 paramValue = new List<ParameterBase>();
		 }

		 public MultiParameterResult(string name, List<ParameterBase> value, ParameterDirection _direction = ParameterDirection.Both)
            : base(name, _direction)
        {
            paramValue = value;
        }

		 List<ParameterBase> paramValue;

        [DataMember]
		 List<ParameterBase> ParamValue
        {
            get { return this.paramValue; }
            set { this.paramValue = value; }
        }

		public  override MiddlewareParameter ToMiddlewareParam()
		{
			MiddlewareParameterDirection mwDirection = (MiddlewareParameterDirection)(int)Direction;
			List<MiddlewareParameter> converted = new List<MiddlewareParameter>();
			paramValue.ForEach(param => {
				StringParameter strParam = param as StringParameter;
				if (strParam != null)
					converted.Add(strParam.ToMiddlewareParam());
				else 
				{
					ByteParameter byteParam = param as ByteParameter;
					if (byteParam != null)
						converted.Add(byteParam.ToMiddlewareParam());
					else
					{
						MultiParameterResult multiParam = param as MultiParameterResult;
						if (multiParam != null)
							converted.Add(multiParam.ToMiddlewareParam());//down the rabbit hole we go
					}
				}
			});
			return new MiddlewareParameter<List<MiddlewareParameter>>(base.ParameterName, converted, mwDirection);
		}
	}
}