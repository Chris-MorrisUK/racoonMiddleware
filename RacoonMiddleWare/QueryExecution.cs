using MiddleWareBussinessObjects;
using StardogConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacoonMiddleWare
{
	public static class QueryExecution
	{
		public static IRacoonResponse ExecuteQueryAllTypes(byte[] token, string query, IEnumerable<IConvertToMiddlewareParam> inputParameterList, ParameterTypeEnum returnTypeWanted,bool addLanuageParam=false)
		{
			Exception error = null;
			IRacoonResponse res = CreateResponse(returnTypeWanted);
			Session currentSession;
			if (SessionStore.TryGetValidSession(token, out currentSession))
			{
				res.AuthorisationOK = true;
				IEnumerable<ParameterBase> responseParameters = null;
                if (addLanuageParam)
                    addLanuageParameter(inputParameterList, currentSession);
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

        private static void addLanuageParameter(IEnumerable<IConvertToMiddlewareParam> inputParameterList,Session currentSession)
        {
            List<IConvertToMiddlewareParam> inputParam = inputParameterList as List<IConvertToMiddlewareParam>;
            if (inputParam != null)
            {
                IConvertToMiddlewareParam languageParameter =  new StringParameter(Consts.LanguageTag, currentSession.Language, ParameterDirection.In);
                inputParam.Add(languageParameter);
            }
        }

		private static IEnumerable<ParameterBase> executeSPARQL(string query, IEnumerable<IConvertToMiddlewareParam> paramList, Session currentSession, ParameterTypeEnum returnWanted, out Exception error)
		{
			StardogQuery queryForStardog = new StardogQuery(query);//if it's a sparql query it's definitely for stardog, not any other data store
			return executeQuery(queryForStardog, paramList, currentSession, returnWanted, out error);
		}

		private static IEnumerable<ParameterBase> executeQuery(IQuerry anyQueryToExecute, IEnumerable<IConvertToMiddlewareParam> paramList, Session currentSession, ParameterTypeEnum returnWanted, out Exception error)
		{
			error = null;
			try
			{
				IEnumerable<MiddlewareParameter> results = anyQueryToExecute.Execute(paramList.ConvertToInternalParameter(), currentSession, returnWanted);
				return results.ConvertToOutput();
			}
			catch (Exception ex)
			{
				error = ex;
				return Enumerable.Empty<ParameterBase>();
			}
		}

		private static IRacoonResponse CreateResponse(ParameterTypeEnum returnWanted)
		{
			if (returnWanted.HasFlag(ParameterTypeEnum.Multivalue))
				return new MultiVariableResponse();//it matters not what the sub type is here
			//it;s a single parameter type
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