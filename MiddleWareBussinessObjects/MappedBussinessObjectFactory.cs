using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
	public static class MappedBussinessObjectFactory
	{
		/// <summary>
		/// Creates a business object and maps populates it using the values in the supplied parameters
		/// </summary>
		/// <typeparam name="T">A business object with a parameterless constructor</typeparam>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static T CreateFromParamers<T>(IEnumerable<MiddlewareParameter> parameters) where T : IMappableBussinessObject
		{
			T bussinessObject = Activator.CreateInstance<T>();
            Type bussinessObjectType = bussinessObject.GetType();
            //Map strings to other strings (only)
            foreach (KeyValuePair<string, string> mapping in bussinessObject.SimpleMappings)
			{
				object fieldValue = getParameterValue(mapping.Key, parameters);
				if (fieldValue == null) continue;//we won't be doing this one. but just fail silently rather than raising an exception				
				PropertyInfo pi = bussinessObjectType.GetProperty(mapping.Value);
				if (pi == null) continue;//much as above, if the field doesn't exist, don't raise an exception 
				pi.SetValue(bussinessObject, fieldValue);
			}

			bussinessObject.DoComplexMappings(parameters);

			return bussinessObject;
		}

		private static object getParameterValue(string name, IEnumerable<MiddlewareParameter> toSearch)
		{
			foreach (MiddlewareParameter param in toSearch)
			{
                if (param.ParamName == name)
                {
                    MiddlewareParameter<string> strParam = param as MiddlewareParameter<string>;
                    if (strParam != null)
                        return strParam.ParamValue;

                    MiddlewareParameter<Uri> uriParam = param as MiddlewareParameter<Uri>;
                    if (uriParam != null)
                        return uriParam.ParamValue;

                    MiddlewareParameter<byte[]> byteParam = param as MiddlewareParameter<byte[]>;
                    if (byteParam != null)
                        return System.Text.Encoding.UTF8.GetString(byteParam.ParamValue);
                    

                }

							
			}
			return null;
		}
	}
}
