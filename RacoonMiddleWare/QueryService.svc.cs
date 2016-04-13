using System;
using System.Collections.Generic;
using MiddleWareBussinessObjects;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StardogConnection;

namespace RacoonMiddleWare
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Racoon : IRacoonService
    {
        public ExecuteQueryResponse ExecuteQuery(byte[] token, string query, IEnumerable<StringParameter> InputParamList)
        {
            return (ExecuteQueryResponse)executeQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.String);
        }

        public ExecuteQueryResponse ExecuteQueryUri(byte[] token, string query, IEnumerable<UriParameter> InputParamList)
        {
            return (ExecuteQueryResponse)executeQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.String);
        }


        public ExecuteQueryBytesResponse ExecuteQueryBytes(byte[] token, string query, IEnumerable<StringParameter> InputParamList)
        {
            return (ExecuteQueryBytesResponse)executeQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.ByteArray);
        }
        public SimpleRacoonResponse InsertBytes(byte[] token, string query, IEnumerable<ByteParameter> InputParamList) 
        {
            return (SimpleRacoonResponse)executeQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.NoExtraData);
        }
   

        public SimpleRacoonResponse InsertString(byte[] token, string query, IEnumerable<StringParameter> InputParamList)
        {
            return (SimpleRacoonResponse)executeQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.NoExtraData);
        }


        private static IRacoonResponse executeQueryAllTypes(byte[] token, string query, IEnumerable<IConvertToMiddlewareParam> inputParameterList, ParameterTypeEnum returnTypeWanted)
        {
            Exception error = null;
            IRacoonResponse res = CreateResponse(returnTypeWanted);
            Session currentSession;
            if (SessionStore.TryGetValidSession(token, out currentSession))
            {
                res.AuthorisationOK = true;
                IEnumerable<ParameterBase> responseParameters = null;
                //ascertain if the passed string is a sp or a sparql query.
                //More complex differentiation could go here to allow things other
                //than stored procedures to be passed to other datastores for example
                if (query.Contains(" "))
                {
                    responseParameters = executeSPARQL(query, inputParameterList, currentSession, returnTypeWanted, out error);
                }
                else
                {
                     responseParameters = executeStoredProcedure(query, inputParameterList, currentSession, returnTypeWanted, out error);                    
                }
                if ((responseParameters != null) && (res is IResponseWithOutput))
                    ((IResponseWithOutput)res).SetOutputParameters(responseParameters);
                res.Error = error;
                res.Status = error == null;
            }
            else
            {
                res.AuthorisationOK = false;
                res.Status = false;
                res.Error = new System.Security.SecurityException("Session invalid or not found");
            }

            return res;
        }

        private static IEnumerable<ParameterBase> executeSPARQL(string query, IEnumerable<IConvertToMiddlewareParam> paramList, Session currentSession, ParameterTypeEnum returnWanted, out Exception error)
        {
            StardogQuery queryForStardog = new StardogQuery(query);//if it's a sparql query it's definitely for stardog, not any other data store
            return executeQuery(queryForStardog, paramList, currentSession, returnWanted, out error);
        }

        private static IEnumerable<ParameterBase> executeQuery(IQuerry stardogQueryToExecute, IEnumerable<IConvertToMiddlewareParam> paramList, Session currentSession, ParameterTypeEnum returnWanted, out Exception error)
        {
            error = null;
            try
            {
                IEnumerable<MiddlewareParameter> results = stardogQueryToExecute.Execute(paramList.ConvertToInternalParameter(), currentSession, returnWanted);
                return results.ConvertToOutput();
            }
            catch (Exception ex)
            {
                error = ex;
                return null;
            }
        }

        private static IRacoonResponse CreateResponse(ParameterTypeEnum returnWanted)
        {
            switch (returnWanted)
            {
                case ParameterTypeEnum.String:
                    return new ExecuteQueryResponse();
                case ParameterTypeEnum.ByteArray:
                    return new ExecuteQueryBytesResponse();
                case ParameterTypeEnum.NoExtraData:
                    return new SimpleRacoonResponse();
            }
            return null;
        }

        private static IEnumerable<ParameterBase> executeStoredProcedure(string query,
            IEnumerable<IConvertToMiddlewareParam> paramList,
            Session currentSession,
            ParameterTypeEnum returnWanted,
            out Exception error)
        {

            StoredProcedure theStoredProc = null;
            if (StoredProcStore.TheStoredProcStore.TryGetSproc(query.GetHashCode(), out theStoredProc))
                return executeQuery(theStoredProc.TheQuerry, paramList, currentSession, returnWanted, out error);

            error = new ArgumentException("Stored Procedure Not Found");
            return null;

        }

    }
}
