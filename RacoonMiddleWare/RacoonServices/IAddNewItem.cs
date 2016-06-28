using RacoonMiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAddNewItem" in both code and config file together.
    [ServiceContract]
    public interface IAddNewItemService
    {
        [OperationContract]
        SimpleRacoonResponse AddNewItem(byte[] token,string graphUri, string ItemURI, string superClassURI, string labelText, string description);
    }
}
