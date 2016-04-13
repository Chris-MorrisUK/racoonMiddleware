using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using MiddleWareBussinessObjects;

namespace RacoonMiddleWare
{
    [DataContract]
    public class ServerDetailsContract
    {
        [DataMember]
        public  string StardogUserName;
        [DataMember]
        public  string StardogPassword;
        [DataMember]
        public  string StardogDB;
        [DataMember]
        public  Uri StardogServer;


        public ServerDetails ToServerDetails()
        {
            return new ServerDetails(
                StardogUserName, 
                StardogPassword, 
                StardogDB, 
                StardogServer
                );
        }
    }

    
}