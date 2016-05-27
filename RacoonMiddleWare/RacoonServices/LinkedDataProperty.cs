using MiddleWareBussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonMiddleWare
{
    [DataContract]
    public class LinkedDataPredicateDataContract: IPopulateFromBO
    {
        [DataMember]
        public string LinkLabel;
        [DataMember]
        public string LinkUri;


        public void Populate(IMappableBussinessObject bo)
        {
            LinkedDataPredicate source = bo as LinkedDataPredicate;
            if (source == null)
                throw new ArgumentException("It is only possible to create a LinkedDataPredicateDataContract from a LinkedDataPredicate");
            this.LinkLabel = source.LinkLabel;
            this.LinkUri = source.LinkUri;
        }
    }
}