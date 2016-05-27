using MiddleWareBussinessObjects;
using RacoonMiddleWare;
using RacoonServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonServices
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetClassesByFreeText" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select GetClassesByFreeText.svc or GetClassesByFreeText.svc.cs at the Solution Explorer and start debugging.
	public class GetClassesByFreeText : RacoonServiceBase, IGetClassesByFreeText
	{
		public GetClassesByFreeTextDataContract GetClassesFreeTextSearch(byte[] token, Uri subgraph, string text)
		{
            
			StringParameter subGraphParam = new StringParameter("@graphToSearch", subgraph.AbsoluteUri, ParameterDirection.In);
			subGraphParam.IsUri = true;
			StringParameter searchTextParam = new StringParameter("@searchText", text, ParameterDirection.In);
			List<IConvertToMiddlewareParam> inputParams = new List<IConvertToMiddlewareParam>();
			inputParams.Add(subGraphParam);
			inputParams.Add(searchTextParam);

			return base.Respond<NamedThing, LabelledItem, GetClassesByFreeTextDataContract>(token, SprocNames.SearchByText, inputParams);			
		}
	}
}
