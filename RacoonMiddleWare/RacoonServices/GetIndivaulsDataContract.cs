using RacoonMiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonServices
{
    [DataContract]
    public class GetIndivaulsDataContract : SimpleRacoonResponse, IResponseWithBussinessObjectEnum
    {
        public GetIndivaulsDataContract()
		{
			ItemsFound = Enumerable.Empty<LabelledItemExtended>();
		}
        public GetIndivaulsDataContract(IRacoonResponse toClone)
		{
			base.CloneToPopulate(toClone);
		}
		[DataMember]
		public IEnumerable<LabelledItemExtended> ItemsFound;

		public void SetOutputList(IEnumerable<IPopulateFromBO> output)
		{
            ItemsFound = output as IEnumerable<LabelledItemExtended>;
		}
    }
}