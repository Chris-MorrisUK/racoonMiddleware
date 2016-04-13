using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;

namespace MiddleWareBussinessObjects
{

    public struct ServerDetails
    {
        public ServerDetails(string stardogUser, string stardogPassword, string stardogdDB, Uri stardogServer)
        {
            StardogUserName = stardogUser;
            StardogPassword = stardogPassword;
            StardogDB = stardogdDB;
            StardogServer = stardogServer;
        }
        public readonly string StardogUserName;
        public readonly string StardogPassword;
        public readonly string StardogDB;
        public readonly Uri StardogServer;
    }
}