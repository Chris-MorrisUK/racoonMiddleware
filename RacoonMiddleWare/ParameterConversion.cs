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

        public static List<StringParameter> ConvertToOutput(this IEnumerable<MiddlewareParameter> middlewareParameters)
        {
            List<StringParameter> results = new List<StringParameter>();
            foreach (MiddlewareParameter current in middlewareParameters)
            {
                StringParameter toAdd = null;
                MiddlewareParameter<string> strParameter = current as MiddlewareParameter<string>;
                if (strParameter != null)
                    toAdd = new StringParameter(strParameter.ParamName, strParameter.ParamValue,ParameterDirection.Out);
                else
                {
                    MiddlewareParameter<byte[]> byteParameter = current as MiddlewareParameter<byte[]>;
                    if (byteParameter != null)
                    {
                        string value = MiddlewareConstants.EncodingInUse.GetString(byteParameter.ParamValue);
                        toAdd = new StringParameter(strParameter.ParamName, strParameter.ParamValue, ParameterDirection.Out);
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