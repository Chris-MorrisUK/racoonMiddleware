using System;
using System.Collections.Generic;
using MiddleWareBussinessObjects;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    /// <summary>
    /// An abstract query, targeting any data store. Includes methods for setting any variables included in the query
    /// </summary>
    public interface IQuerry
    {
        void SetTarget(string server,string datastore);
        void SetQuerry(string queryText);
        IEnumerable<MiddlewareParameter> Execute(IEnumerable<MiddlewareParameter> parameters, Session session, ParameterTypeEnum returnTypeWanted);
        //void SetDataStore(object store);
        
    }
}
