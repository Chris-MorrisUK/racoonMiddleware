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
    public enum ParameterTypeEnum
    {
        String,
        ByteArray,
        Uri,
        /// <summary>
        /// All we are returning here is error information: it's for insert queries
        /// </summary>
        NoExtraData,
        Unknown
    }
}
