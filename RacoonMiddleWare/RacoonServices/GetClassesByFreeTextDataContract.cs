using RacoonMiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonServices
{
	[DataContract]
	public class GetClassesByFreeTextDataContract :SimpleRacoonResponse, IResponseWithBussinessObjectEnum
	{
		public GetClassesByFreeTextDataContract()
		{
			ItemsFound = Enumerable.Empty<LabelledItem>();
		}
		public GetClassesByFreeTextDataContract(IRacoonResponse toClone)
		{
			base.CloneToPopulate(toClone);
		}
		[DataMember]
		public IEnumerable<LabelledItem> ItemsFound;

		public void SetOutputList(IEnumerable<IPopulateFromBO> output)
		{
			ItemsFound = output as IEnumerable<LabelledItem>;
		}
	}
}