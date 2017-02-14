using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonMiddleWare
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHighFrequencyDataService" in both code and config file together.
    [ServiceContract]
    public interface IHighFrequencyDataService
    {
        [OperationContract]
        SimpleRacoonResponse InsertObject(byte[] token, string key, byte[] value);

        [OperationContract]
        RetrieveFromREDISResponse RetrieveOject(byte[] token, string key);
    }
}
