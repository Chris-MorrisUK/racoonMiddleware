using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardogConnection
{
    public static class StardogQueryTypeProcess
    {
        public static StardogQueryTypeEnum GetQueryType(string query)
        {
            if (query.Contains("SELECT "))
                return StardogQueryTypeEnum.SELECT;
            if (query.Contains("INSERT "))
                return StardogQueryTypeEnum.INSERT;
            return StardogQueryTypeEnum.Unknown;
        }
    }
}
