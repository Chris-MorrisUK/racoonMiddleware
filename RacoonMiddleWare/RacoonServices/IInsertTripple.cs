using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonMiddleWare.RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IInsertTripple" in both code and config file together.
    [ServiceContract]
    public interface IInsertTripple
    {
        [OperationContract]
        SimpleRacoonResponse InsertTriple(byte[] token, List<InsertableTriple> DataToInsert, string graph);

        [OperationContract]
        SimpleRacoonResponse InsertTripleSimple(byte[] token, InsertableTriple[] DataToInsert, string graph);

        [OperationContract]
        SimpleRacoonResponse InsertSingleTriple(byte[] token, string subj, string pred, string obj, string graph);
    }
}
