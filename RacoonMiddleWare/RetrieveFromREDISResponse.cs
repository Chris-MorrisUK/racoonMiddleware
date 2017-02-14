using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonMiddleWare
{
    [DataContract]
    public class RetrieveFromREDISResponse : SimpleRacoonResponse
    {
        [DataMember]
        public string SerializedObject;
    }
}