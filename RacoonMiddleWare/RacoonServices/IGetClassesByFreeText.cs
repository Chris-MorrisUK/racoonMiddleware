using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonServices
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetClassesByFreeText" in both code and config file together.
	[ServiceContract]
	public interface IGetClassesByFreeText
	{
		[OperationContract]
		GetClassesByFreeTextDataContract  GetClassesFreeTextSearch(byte[] token,Uri subgraph,string text);
	}
}
