using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
	public interface IMappableBussinessObject
	{
		/// <summary>
		/// This is mapping of string parameters to string values, with no manipulation
		/// Anything else has to be done by hand in DoComplexMappings.
		/// The aim of this is to prevent try and avoid excessive repetition
		/// </summary>
        /// <remarks>
        /// The biggest gotcha when using this is that SimpleMappings must be PROPERTIES. Not fields. so use a {get; set;} if it's not otherwise a property
        /// </remarks>
		List<KeyValuePair<string, string>> SimpleMappings { get;  }

		/// <summary>
		/// Any mapping not covered by simple mappings
		/// </summary>
		/// <param name="toMap">The middleware parameters can be of any type - the generic MiddleParmeter<T> extends MiddlewareParameter</param>
		void DoComplexMappings(IEnumerable<MiddlewareParameter> toMap);
	}

	
}
