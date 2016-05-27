using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using RacoonMiddleWare;

namespace RacoonServices
{
    [DataContract]
    public class ExpectedPredicateDataContract : SimpleRacoonResponse, IResponseWithBussinessObjectEnum
    {
        public ExpectedPredicateDataContract()
        {
            this.PossiblePredicates = Enumerable.Empty<LinkedDataPredicateDataContract>();
        }
        public ExpectedPredicateDataContract(SimpleRacoonResponse simple)
            : this()
        {
            base.CloneToPopulate(simple);
        }

        [DataMember]
        public IEnumerable<LinkedDataPredicateDataContract> PossiblePredicates;

        public void SetOutputList(IEnumerable<IPopulateFromBO> output)
        {
            PossiblePredicates = output as IEnumerable<LinkedDataPredicateDataContract>;
        }
    }
}