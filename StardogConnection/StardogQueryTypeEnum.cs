using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardogConnection
{
    public enum StardogQueryTypeEnum
    {
        /// <summary>
        /// In this case the query object trys to work it out from the text
        /// Included for compatability
        /// </summary>
        Unknown,
        SELECT,
        INSERT
    }
}
