using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MiddleWareBussinessObjects;
using RacoonMiddleWare;

namespace RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GetIndivauls" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GetIndivauls.svc or GetIndivauls.svc.cs at the Solution Explorer and start debugging.
    public class GetIndividualsService :RacoonServiceBase, IGetIndividuals
    {
        public GetIndivaulsDataContract GetIndividuals(byte[] token, string className)
        {
            StringParameter classNameParam = new StringParameter("@class", className, ParameterDirection.In);
            classNameParam.IsUri = true;
            List<IConvertToMiddlewareParam> inputParams = new List<IConvertToMiddlewareParam>() { classNameParam };
            return base.Respond<NamedThingDetail, LabelledItemExtended, GetIndivaulsDataContract>(token, SprocNames.GetIndivauls, inputParams);
        }
    }
}
