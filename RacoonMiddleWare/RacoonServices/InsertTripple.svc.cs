using MiddleWareBussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonMiddleWare.RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "InsertTripple" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select InsertTripple.svc or InsertTripple.svc.cs at the Solution Explorer and start debugging.
    public class InsertTripple : IInsertTripple
    {
        public SimpleRacoonResponse InsertTriple(byte[] token, List<InsertableTriple> DataToInsert, string graph)
        {
            
            Exception error = null;
            SimpleRacoonResponse res = new SimpleRacoonResponse();
            if (DataToInsert.Count == 0)
            {
                res.AuthorisationOK = true;//we don't know this, but nor do we wish to trigger anything around dealing with aurthorisation problems
                res.Error = new ArgumentException("Must pass in some data to insert");
                res.Status = false;
                return res;
            }
            Session currentSession;
            if (SessionStore.TryGetValidSession(token, out currentSession))
            {
                error = InsertData(DataToInsert, graph, currentSession);
                QueryExecution.SuccessResponse(res, error);                
            }
            else
            {
                QueryExecution.SecurityFailureResponse(res);
            }
            return res;
        }

        public static Exception InsertData(List<InsertableTriple> DataToInsert, string graph, Session currentSession)
        {
            Exception error;
            StringBuilder QueryBuilder = new StringBuilder(QueryStart);
            QueryBuilder.AppendLine("<" + graph + ">");
            QueryBuilder.AppendLine("{");
            foreach (InsertableTriple tripple in DataToInsert)
                QueryBuilder.AppendLine(tripple.ToSparqlLine(currentSession));
            QueryBuilder.Append("} } ");
            QueryExecution.executeSPARQL(QueryBuilder.ToString(), Enumerable.Empty<IConvertToMiddlewareParam>(), currentSession, ParameterTypeEnum.NoExtraData, out error);
            return error;
        }



        const string QueryStart = "INSERT DATA \r\n { GRAPH ";
    }
}
