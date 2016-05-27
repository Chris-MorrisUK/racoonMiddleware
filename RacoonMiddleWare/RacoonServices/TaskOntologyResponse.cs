using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using RacoonMiddleWare;

namespace RacoonServices
{
	[DataContract]
	public class TaskOntologyResponse : SimpleRacoonResponse, IResponseWithBussinessObjectEnum
	{
		public TaskOntologyResponse()
		{
			this.TaskOntologies = Enumerable.Empty<TaskOntologyDataContract>();
		}
		public TaskOntologyResponse(IRacoonResponse simple): this()
		{
			base.CloneToPopulate(simple);
		}

		[DataMember]
		public IEnumerable<TaskOntologyDataContract> TaskOntologies;

		public void SetOutputList(IEnumerable<IPopulateFromBO> output)
		{
			TaskOntologies = output as IEnumerable<TaskOntologyDataContract>;
		}
	}
}