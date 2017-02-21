using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGetIndivauls" in both code and config file together.
    [ServiceContract]
    public interface IGetIndividuals
    {
        [OperationContract]
        GetIndivaulsDataContract GetIndividuals(byte[] token, string className);
        
    }
}
