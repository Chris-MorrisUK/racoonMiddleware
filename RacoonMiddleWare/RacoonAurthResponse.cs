using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace RacoonMiddleWare
{
    [DataContract]
    public class RacoonAurthorisationResponse: SimpleRacoonResponse
    {
        [DataMember]
        public byte[] Token;
    }
}

