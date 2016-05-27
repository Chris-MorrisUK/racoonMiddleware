using MiddleWareBussinessObjects;
using RacoonMiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetPredicatesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetPredicatesService.svc or GetPredicatesService.svc.cs at the Solution Explorer and start debugging.
    public class GetPredicatesService :RacoonServiceBase, IGetPredicatesService
    {
        public ExpectedPredicateDataContract GetPredicates(byte[] token,string superclass)
        {
            StringParameter searchTextParam = new StringParameter("@superClass", superclass, ParameterDirection.In);
            List<IConvertToMiddlewareParam> inputParams = new List<IConvertToMiddlewareParam>();
            inputParams.Add(searchTextParam);
            return base.Respond<LinkedDataPredicate, LinkedDataPredicateDataContract, ExpectedPredicateDataContract>(token, SprocNames.GetPredicates, inputParams);
        }
    }
}
