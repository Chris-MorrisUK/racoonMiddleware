using RacoonMiddleWare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MiddleWareBussinessObjects.LDLFileBO;
using MiddleWareBussinessObjects;
using RacoonMiddleWare.RacoonServices;

namespace RacoonServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LoadLDLFile" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LoadLDLFile.svc or LoadLDLFile.svc.cs at the Solution Explorer and start debugging.
    [KnownType(typeof(SimpleRacoonResponse))]
    public class LoadLDLFileService : RacoonServiceBase, ILoadLDLFile
    {
        public SimpleRacoonResponse LoadLDLFile(byte[] token, string[] LDLFileContent, string[] AbsolutePosContent, string graph)
        {
            Exception error = null;
            SimpleRacoonResponse res = new SimpleRacoonResponse();
            Session currentSession;
            if (SessionStore.TryGetValidSession(token, out currentSession))
            {
                try
                {
                    LDLParser theParser = LDLParser.GetParser();
                    theParser.ParseText(LDLFileContent, AbsolutePosContent);
                    IEnumerable<BOTripple> tripples = theParser.GetAsTripples();
                    List<InsertableTriple> toInsert = new List<InsertableTriple>();
                    foreach (BOTripple trip in tripples)
                        toInsert.Add(new InsertableTriple(trip));
                    error = InsertTripple.InsertData(toInsert, graph, currentSession);
                }
                catch (Exception ex)
                {
                    error = ex;
                }
                QueryExecution.SuccessResponse(res, error);
            }
            else
            {
                QueryExecution.SecurityFailureResponse(res);
            }

            return res;

        }


    }
}
