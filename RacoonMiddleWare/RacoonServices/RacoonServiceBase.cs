using MiddleWareBussinessObjects;
using RacoonMiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RacoonServices
{
	public class RacoonServiceBase
	{
		protected IRacoonResponse TryExecuteQueryMultiVariable(byte[] token, string sproc, IEnumerable<IConvertToMiddlewareParam> inputParams)
		{
			IRacoonResponse executeResponse = QueryExecution.ExecuteQueryAllTypes(token, sproc, inputParams, ParameterTypeEnum.AsSource | ParameterTypeEnum.Multivalue);
			if (!executeResponse.AuthorisationOK)
				return executeResponse;
			MultiVariableResponse queryRes = executeResponse as MultiVariableResponse;
			if (queryRes == null)
			{				
				executeResponse.Status = false;
				executeResponse.Error = new InvalidCastException("Unexpected return type from query");				
			}
			return executeResponse;

		}

		protected List<DCType> CreateBussinessObjectsFromResponse<BOType, DCType>(MultiVariableResponse response)
			where BOType : IMappableBussinessObject
			where DCType : IPopulateFromBO, new()
		{
			List<DCType> createdBO = new List<DCType>();
			foreach (MultiParameterResult parameterGroup in response.OutputParameters)
			{                
				if (parameterGroup.Direction == ParameterDirection.Out)
				{
                    //foreach (ParameterBase paramValue in parameterGroup.ParamValue)
                    //{
                    //    Type paramValueType = paramValue.GetType();
                    //    MiddlewareParameter<paramValueType.GetType()> convertedParameter = paramValue.ToMiddlewareParam();                        
                    //    convertedParameter.ParamValue
                    //    BOType availableBO = MappedBussinessObjectFactory.CreateFromParamers<BOType>();
                    //    DCType dc = new DCType();
                    //    dc.Populate(availableBO);
                    //    createdBO.Add(dc);
                    //}
                    MiddlewareParameter<List<MiddlewareParameter>> convertedParameter = (MiddlewareParameter<List<MiddlewareParameter>>)((MultiParameterResult)parameterGroup).ToMiddlewareParam();
                    BOType availableBO = MappedBussinessObjectFactory.CreateFromParamers<BOType>(convertedParameter.ParamValue);
                    DCType dc = new DCType();
                    dc.Populate(availableBO);
                    createdBO.Add(dc);
                }
			}
			return createdBO;
		}

		protected ResposeType Respond<BOType, DCType, ResposeType>(byte[] token, string sproc, IEnumerable<IConvertToMiddlewareParam> inputParams)
			where BOType : IMappableBussinessObject
			where DCType : IPopulateFromBO, new()
			where ResposeType : SimpleRacoonResponse, IResponseWithBussinessObjectEnum, new()
		{
			IRacoonResponse queryRes = TryExecuteQueryMultiVariable(token, sproc, inputParams);
			ResposeType response = new ResposeType();
			response.CloneToPopulate(queryRes);
			if (!queryRes.AuthorisationOK || !queryRes.Status)
				return response			;
			MultiVariableResponse multiVarRes = queryRes as MultiVariableResponse;            
			List<DCType> dataContractItems = CreateBussinessObjectsFromResponse<BOType, DCType>(multiVarRes);
			if (dataContractItems.Count > 0)
				response.SetOutputList(dataContractItems as IEnumerable<IPopulateFromBO>);
			return response;
		}

	}
}