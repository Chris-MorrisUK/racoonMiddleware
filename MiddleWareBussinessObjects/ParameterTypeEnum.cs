using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    /// <summary>
    /// The type of a parameter
    /// </summary>
    /// <remarks>
    /// As and when other MiddlewareParameter types are added put a reference here so that they can be returned
    /// </remarks>
	[Flags]
    public enum ParameterTypeEnum
    {
		/// <summary>
		/// All we are returning here is error information: it's for insert queries
		/// </summary>
		NoExtraData=0,
        String=1,
        ByteArray=2,
        Uri=4,	
		Multivalue=8
    }
}
