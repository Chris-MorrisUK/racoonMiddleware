using System;
using System.Collections.Generic;
using MiddleWareBussinessObjects;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonMiddleWare
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AurthenticateService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AurthenticateService.svc or AurthenticateService.svc.cs at the Solution Explorer and start debugging.
    public class AurthenticateService : IAurthenticateService
    {
        public RacoonAurthorisationResponse Authenticate(string userName, string password, string stardogUser, string stardogPassword, Uri stardogServer, string stardogDatastore,string language)
        {
            RacoonAurthorisationResponse response = new RacoonAurthorisationResponse();
            try
            {
                response.Token = Auth.GetToken(userName, password);
                if (response.Token != null)
                {
                    SessionStore.CreateAndAddSession(response.Token, new ServerDetails(stardogUser, stardogPassword, stardogDatastore, stardogServer), language);
                    response.AuthorisationOK = true;
                }
                else
                {
                    response.AuthorisationOK = false;
                }
            }
            catch (Exception exp)
            {
                response.Error = exp;
            }
            response.Status = response.Error == null;
            return response;
        
        }
         
    }
}
