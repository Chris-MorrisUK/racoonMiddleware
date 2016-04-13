using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacoonMiddleWare
{
    /// <summary>
    /// An abstract query, targeting any data store. Includes methods for setting any varables included in the query
    /// </summary>
    interface IQuerry
    {
        void SetQuerry(string queryText);
        object Execute(params object[] parameters);
        void SetDataStore(object store);
        
    }
}
