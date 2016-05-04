using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using RacoonMiddleWare;

namespace RacoonServices
{
	[DataContract]
	public class TaskOntologyResponse: SimpleRacoonResponse
	{
		public TaskOntologyResponse()
		{
			this.TaskOntologies = Enumerable.Empty<TaskOntologyDataContract>();
		}
		public TaskOntologyResponse(IRacoonResponse simple): this()
		{
			this.AuthorisationOK = simple.AuthorisationOK;
			this.Error = simple.Error;
			this.Status = simple.Status;
		}

		[DataMember]
		public IEnumerable<TaskOntologyDataContract> TaskOntologies;
		

	}
}