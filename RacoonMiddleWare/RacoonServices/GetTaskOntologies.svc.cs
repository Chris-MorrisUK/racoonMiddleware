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
	public class GetTaskOntologiesService : RacoonServiceBase, IGetTaskOntologies
	{
		public TaskOntologyResponse GetTaskOntologies(byte[] token)
		{
			return base.Respond<TaskOntology, TaskOntologyDataContract, TaskOntologyResponse>
				(token, SprocNames.GetTaskOntologies, Enumerable.Empty<IConvertToMiddlewareParam>());
		}


	}
}
