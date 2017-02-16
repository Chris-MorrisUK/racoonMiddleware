using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MiddleWareBussinessObjects;

namespace RacoonMiddleWare
{
    public class CheckParameters
    {
        public static IEnumerable<IConvertToMiddlewareParam> CheckParameterDirection(IEnumerable<StringParameter> strings)
        {
            foreach (StringParameter strParam in strings)
            {
                if(strParam != null)
                    if (string.IsNullOrEmpty(strParam.ParamValue))
                        strParam.Direction = ParameterDirection.Out;
            }
            return strings;
        }

        public static IEnumerable<IConvertToMiddlewareParam> CheckParameterDirection(IEnumerable<UriParameter> uris)
        {
            foreach (UriParameter uriParam in uris)
            {
                if(uriParam != null)
                    if (uriParam.ParamValue == null)
                        uriParam.Direction = ParameterDirection.Out;
            }
            return uris;
        }
    }
}