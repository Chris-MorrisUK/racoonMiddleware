using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RacoonMiddleWare
{
	[DataContract(Name = "ParameterDirection")]
	public enum ParameterDirection
	{
		[EnumMember]
		In = 0,
		[EnumMember]
		Out = 1,
		[EnumMember]
		Both = 2
	}
}
