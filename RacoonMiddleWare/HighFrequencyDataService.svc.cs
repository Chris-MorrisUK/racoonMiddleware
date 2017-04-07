using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using REDISConnector;
using MiddleWareBussinessObjects;

namespace RacoonMiddleWare
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HighFrequencyDataService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HighFrequencyDataService.svc or HighFrequencyDataService.svc.cs at the Solution Explorer and start debugging.
    public class HighFrequencyDataService : IHighFrequencyDataService
    {
        public SimpleRacoonResponse InsertObject(byte[] token, string key, string value)
        {           
            SimpleRacoonResponse res = new SimpleRacoonResponse();
            Session currentSession;
            if (SessionStore.TryGetValidSession(token, out currentSession))
            {
                Exception error = null;
                try
                {
                    REDISConnector.REDISConnector.SetValue(key, value);
                }
                catch (Exception exp)
                {
                    error = exp;
                }
                QueryExecution.SuccessResponse(res, error);
            }
            else
            {
               QueryExecution.SecurityFailureResponse(res);
            }
            return res;             
        }

        public RetrieveFromREDISResponse RetrieveOject(byte[] token, string key)
        {
            RetrieveFromREDISResponse res = new RetrieveFromREDISResponse();
            Session currentSession;
            if (SessionStore.TryGetValidSession(token, out currentSession))
            {
                Exception error = null;
                try
                {
                    res.SerializedObject = REDISConnector.REDISConnector.GetValue(key);
                }
                catch (Exception exp)
                {
                    error = exp;
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
