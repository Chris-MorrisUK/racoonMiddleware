using MiddleWareBussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacoonMiddleWare
{
    public static class ParameterConversion
    {
        public static IEnumerable<MiddlewareParameter> ConvertToInternalParameter(this IEnumerable<IConvertToMiddlewareParam> paramList)
        {
            List<MiddlewareParameter> res = new List<MiddlewareParameter>();
            if (paramList != null)//It's perfectly reasonable not to have any parameters
            {
                foreach (IConvertToMiddlewareParam p in paramList)
                    if (p != null)
                        res.Add(p.ToMiddlewareParam());
            }
            return res;
        }

		public static IEnumerable<ParameterBase> ConvertToOutput(this IEnumerable<MiddlewareParameter> middlewareParameters)
        {
			List<ParameterBase> results = new List<ParameterBase>();
            foreach (MiddlewareParameter current in middlewareParameters)
            {
                ParameterBase toAdd = null;
                MiddlewareParameter<string> strParameter = current as MiddlewareParameter<string>;
                if (strParameter != null)
                    toAdd = new StringParameter(strParameter.ParamName, strParameter.ParamValue,ParameterDirection.Out);
                else
                {
                    MiddlewareParameter<byte[]> byteParameter = current as MiddlewareParameter<byte[]>;
                    if (byteParameter != null)
                    {
                        //string value = MiddlewareConstants.EncodingInUse.GetString(byteParameter.ParamValue);
                        toAdd = new ByteParameter(byteParameter.ParamName, byteParameter.ParamValue, ParameterDirection.Out);
                    }
                }
				if (toAdd == null)//still hasn't managed
				{
					MiddlewareParameter<List<MiddlewareParameter>> multiParam = current as MiddlewareParameter<List<MiddlewareParameter>>;
					if (multiParam != null)
					{
						toAdd = new MultiParameterResult(multiParam.ParamName,(List<ParameterBase>)multiParam.ParamValue.ConvertToOutput(),ParameterDirection.Out);
						
					}
				}
                if (toAdd == null)
                    throw new InvalidOperationException("Parameter not of acceptable type");
                results.Add(toAdd);
            }
            return results;
        }
    }
}