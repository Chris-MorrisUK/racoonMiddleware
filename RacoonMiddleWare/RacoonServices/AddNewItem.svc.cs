using System;
using System.Collections.Generic;
using RacoonMiddleWare;
using MiddleWareBussinessObjects;

namespace RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AddNewItem" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AddNewItem.svc or AddNewItem.svc.cs at the Solution Explorer and start debugging.
    public class AddNewItemService : IAddNewItemService
    {
        public SimpleRacoonResponse AddNewItem(byte[] token,string graphUri, string ItemURI, string superClassURI, string labelText, string description)
        {            
            List<ParameterBase> InputParamList = new List<ParameterBase>(){
                new UriParameter("graph",new Uri(graphUri),ParameterDirection.In),
                new UriParameter("itemURI", new Uri(ItemURI), ParameterDirection.In),
                new UriParameter("superTypeURI", new Uri(superClassURI), ParameterDirection.In),
                new StringParameter("label", labelText, ParameterDirection.In),
                new StringParameter("description", description, ParameterDirection.In)
            };
            return (SimpleRacoonResponse)QueryExecution.ExecuteQueryAllTypes(token, SprocNames.InsertClass, InputParamList, ParameterTypeEnum.NoExtraData);
        }
    }
}
