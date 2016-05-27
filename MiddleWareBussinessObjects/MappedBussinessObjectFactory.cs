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
			//Map strings to other strings (only)
			foreach (KeyValuePair<string, string> mapping in bussinessObject.SimpleMappings)
			{
				string fieldValue = getParameterValue(mapping.Key, parameters);
				if (fieldValue == null) continue;//we won't be doing this one. but just fail silently rather than raising an exception
				Type bussinessObjectType = bussinessObject.GetType();
				PropertyInfo pi = bussinessObjectType.GetProperty(mapping.Value);
				if (pi == null) continue;//much as above, if the field doesn't exist, don't raise an exception 
				pi.SetValue(bussinessObject, fieldValue);
			}

			bussinessObject.DoComplexMappings(parameters);

			return bussinessObject;
		}

		private static string getParameterValue(string name, IEnumerable<MiddlewareParameter> toSearch)
		{
			foreach (MiddlewareParameter param in toSearch)
			{
				MiddlewareParameter<string> strParam = param as MiddlewareParameter<string>;
				if (strParam == null) continue;
				if (strParam.ParamName == name)
					return strParam.ParamValue;				
			}
			return null;
		}
	}
}
