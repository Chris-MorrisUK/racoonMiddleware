using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiddleWareBussinessObjects;
using REDISConnector;
using StackExchange.Redis;

namespace Queries
{
    public class RedisQuery : IQuerry
    {
                
        string queryText = string.Empty;
        string server = string.Empty;
        int datastore = 0;
        LuaScript prepared;
        LoadedLuaScript loaded;



        #region IQuerry Members


        public void SetTarget(string _server, string _datastore)
        {
            server = _server;
            datastore = int.Parse(_datastore);
        }

        public void SetQuerry(string _queryText)
        {
            queryText = _queryText;
            prepared = LuaScript.Prepare(queryText);

            loaded= prepared.Load(REDISConnector.REDISConnector.GetRedis().GetServer(server));
        }

        public IEnumerable<MiddlewareParameter> Execute(IEnumerable<MiddlewareParameter> parameters, Session session, ParameterTypeEnum returnTypeWanted)
        {


            IDatabase db = REDISConnector.REDISConnector.GetRedis().GetDatabase(datastore);
           // loaded.Evaluate(db, convertParamNames(parameters),convertParamValues(parameters));
            RedisResult res = db.ScriptEvaluate(loaded.Hash, convertParamNames(parameters), convertParameterValues(parameters));
            return new List<MiddlewareParameter> { new MiddlewareParameter<string>("RedisResult", res.ToString(), MiddlewareParameterDirection.Out) };
            
            
        }
        private static RedisValue[] convertParameterValues(IEnumerable<MiddlewareParameter> parameters)
        {
            List<RedisValue> converted = new List<RedisValue>();
            foreach (MiddlewareParameter param in parameters)
            {
                converted.Add(convertSingleParameterValue(param));
            }
            return converted.ToArray();
        }
        private static RedisKey[] convertParamNames(IEnumerable<MiddlewareParameter> parameters)
        {
            List<RedisKey> converted = new List<RedisKey>();
            foreach (MiddlewareParameter param in parameters)
            {
                converted.Add(param.ParamName);
            }
            return converted.ToArray();
        }
        
        private static RedisValue convertSingleParameterValue(MiddlewareParameter parameter)
        {
            MiddlewareParameter<string> strParam = parameter as MiddlewareParameter<string> ;
            if (strParam != null)
                return strParam.ParamValue;

            MiddlewareParameter<byte[]> byteParam = parameter as MiddlewareParameter<byte[]>;
            if (byteParam != null)
                return byteParam.ParamValue;

            MiddlewareParameter<Uri> uriParam = parameter as MiddlewareParameter<Uri>;
            if (uriParam != null)
                return uriParam.ParamValue.AbsolutePath;

            throw new InvalidCastException("The parameter is not of a permitted type");
        }

        #endregion


        //This only exists for debug purposes
        public static string GetAssembly()
        {
            string fullyQualifiedName = typeof(RedisQuery).AssemblyQualifiedName;
            return fullyQualifiedName;
        }
    }
}
