using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RacoonMiddleWare
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAurthenticateService" in both code and config file together.
    [ServiceContract]
    public interface IAurthenticateService
    {
        /// <summary>
        /// Call this at the start of a session to authenticate.
        /// </summary>
        /// <param name="userNameHash"></param>
        /// <param name="passwordHash"></param>
        /// <returns>A token to log in or the empty string if authentication fails</returns>
        [OperationContract]
        RacoonAurthorisationResponse Authenticate(string userName, string password, string stardogUser, string stardogPassword, Uri stardogServer, string stardogDatastore);
    }
}
