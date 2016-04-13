using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RacoonMiddleWare
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRacoonService
    {

        [OperationContract]
        ExecuteQueryResponse ExecuteQuery(byte[] token, string query, IEnumerable<StringParameter> InputParamList);

        [OperationContract]
        ExecuteQueryResponse ExecuteQueryUri(byte[] token, string query, IEnumerable<UriParameter> InputParamList);

        [OperationContract]
        ExecuteQueryBytesResponse ExecuteQueryBytes(byte[] token, string query, IEnumerable<StringParameter> InputParamList);

        [OperationContract]
        SimpleRacoonResponse InsertBytes(byte[] token, string query, IEnumerable<ByteParameter> InputParamList);

        [OperationContract]
        SimpleRacoonResponse InsertString(byte[] token, string query, IEnumerable<StringParameter> InputParamList);

  
    }



}
