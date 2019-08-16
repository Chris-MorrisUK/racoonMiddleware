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
            return InsertTripleSimple(token, DataToInsert.ToArray<InsertableTriple>(), graph);
        }

        public static Exception InsertData(IEnumerable<InsertableTriple> DataToInsert, string graph, Session currentSession)
        {
            Exception error;
            StringBuilder QueryBuilder = createQueryBuilder(graph);
            foreach (InsertableTriple tripple in DataToInsert)
                QueryBuilder.AppendLine(tripple.ToSparqlLine(currentSession));
            QueryBuilder.Append("} } ");
            QueryExecution.executeSPARQL(QueryBuilder.ToString(), Enumerable.Empty<IConvertToMiddlewareParam>(), currentSession, ParameterTypeEnum.NoExtraData, out error);
            return error;
        }

        public static Exception InsertSingleTripleExec(string subj,string pred,string obj, string graph, Session currentSession)
        {
            Exception error;
            StringBuilder QueryBuilder = createQueryBuilder(graph);
            QueryBuilder.AppendFormat(" {0} {1} {2} ", subj, pred, obj);
            QueryBuilder.Append("}  } ");
            QueryExecution.executeSPARQL(QueryBuilder.ToString(), Enumerable.Empty<IConvertToMiddlewareParam>(), currentSession, ParameterTypeEnum.NoExtraData, out error);
            return error;
        }

        private static StringBuilder createQueryBuilder(string graph)
        {
            StringBuilder QueryBuilder = new StringBuilder(QueryStart);
            QueryBuilder.AppendLine("<" + graph + ">");
            QueryBuilder.AppendLine("{");
            return QueryBuilder;
        }

        public SimpleRacoonResponse InsertTripleSimple(byte[] token, InsertableTriple[] DataToInsert, string graph)
        {
            Exception error = null;
            SimpleRacoonResponse res = new SimpleRacoonResponse();
            if (DataToInsert.Length == 0)
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

        public SimpleRacoonResponse InsertSingleTriple(byte[] token,string subj,string pred,string obj, string graph)
        {
            Exception error = null;
            SimpleRacoonResponse res = new SimpleRacoonResponse();

            Session currentSession;
            if (SessionStore.TryGetValidSession(token, out currentSession))
            {
                error = InsertSingleTripleExec(subj, pred, obj, graph, currentSession);
                QueryExecution.SuccessResponse(res, error);
            }
            else
            {
                QueryExecution.SecurityFailureResponse(res);
            }
            return res;
        }

        const string QueryStart = "INSERT DATA \r\n { GRAPH ";
    }
}
