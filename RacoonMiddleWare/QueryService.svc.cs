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
    public class QueryService : IRacoonQueryService
    {
		public MultiVariableResponse ExecuteQueryMultiString(byte[] token, string query, IEnumerable<StringParameter> InputParamList)
		{
			return (MultiVariableResponse)QueryExecution.ExecuteQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.String | ParameterTypeEnum.Multivalue);
		}

        public ExecuteQueryResponse ExecuteQuery(byte[] token, string query, IEnumerable<StringParameter> InputParamList)
        {
			return (ExecuteQueryResponse)QueryExecution.ExecuteQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.String);
        }

        public ExecuteQueryResponse ExecuteQueryUri(byte[] token, string query, IEnumerable<UriParameter> InputParamList)
        {
			return (ExecuteQueryResponse)QueryExecution.ExecuteQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.String);
        }


        public ExecuteQueryBytesResponse ExecuteQueryBytes(byte[] token, string query, IEnumerable<StringParameter> InputParamList)
        {
			return (ExecuteQueryBytesResponse)QueryExecution.ExecuteQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.ByteArray);
        }
        public SimpleRacoonResponse InsertBytes(byte[] token, string query, IEnumerable<ByteParameter> InputParamList) 
        {
			return (SimpleRacoonResponse)QueryExecution.ExecuteQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.NoExtraData);
        }
   

        public SimpleRacoonResponse InsertString(byte[] token, string query, IEnumerable<StringParameter> InputParamList)
        {
			return (SimpleRacoonResponse)QueryExecution.ExecuteQueryAllTypes(token, query, InputParamList, ParameterTypeEnum.NoExtraData);
        }


        

    }
}
