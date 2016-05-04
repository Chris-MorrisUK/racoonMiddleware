using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using RacoonMiddleWare;
using MiddleWareBussinessObjects;

namespace RacoonServices
{
	public class GetTaskOntologiesService : IGetTaskOntologies
	{
		public TaskOntologyResponse GetTaskOntologies(byte[] token)
		{			
			IRacoonResponse executeResponse = QueryExecution.ExecuteQueryAllTypes(token, SprocNames.GetTaskOntologies, Enumerable.Empty<IConvertToMiddlewareParam>(), ParameterTypeEnum.String & ParameterTypeEnum.Multivalue);
			TaskOntologyResponse response = new TaskOntologyResponse(executeResponse);
			if (!response.AuthorisationOK)
				return response;
			MultiVariableResponse queryRes = executeResponse as MultiVariableResponse;
			if (queryRes == null)
			{
				response.Status = false;
				response.Error = new InvalidCastException("Unexpected return type from query");
				return response;
			}
			else
			{
				List<TaskOntologyDataContract> returnableTaskOntologyList = new List<TaskOntologyDataContract>();
				foreach (MultiParameterResult param in queryRes.OutputParameters)
				{
					if (param.Direction == ParameterDirection.Out)
					{
						MiddlewareParameter<List<MiddlewareParameter>> convertedParameter = (MiddlewareParameter<List<MiddlewareParameter>>)param.ToMiddlewareParam();
						TaskOntology availableTaskOntology = MappedBussinessObjectFactory.CreateFromParamers<TaskOntology>(convertedParameter.ParamValue);
						TaskOntologyDataContract returnableTaskOntology = new TaskOntologyDataContract(availableTaskOntology);
						returnableTaskOntologyList.Add(returnableTaskOntology);
					}
				}
				if (returnableTaskOntologyList.Count > 0)
					response.TaskOntologies = returnableTaskOntologyList;
			}

			return response;
		}


	}
}
