using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using REDISConnector;
using BCrypt.Net;
using System.Security.Cryptography;
using MiddleWareBussinessObjects;

namespace RacoonMiddleWare
{
    public class Auth
    {
        public static byte[] GetToken(string userName,string password)
           {
            UserBase userLoggingOn = UserStore.GetUser(userName);
            if(userLoggingOn == null)
                return null;
            if (BCrypt.Net.BCrypt.Verify(password, userLoggingOn.HashedPassword))            
                return TokenStore.CreateAndAddToken(userLoggingOn);            
            else
                return null;                           
        }

        





        
    }
}