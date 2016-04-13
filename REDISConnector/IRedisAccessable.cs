using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REDISConnector
{
    public interface IRedisAccessable
    {
         string RedisKey
        {
            get;
        }
    }
}
