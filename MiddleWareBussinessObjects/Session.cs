using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    /// <summary>
    /// This stores session information. One session is generated per user per application
    /// It holds details that should reside in memory and need not (or must not for security reasons) be added to REDIS
    /// </summary>
    /// <remarks>
    /// If you need to add more session information this is the place to do it
    /// </remarks>
    public class Session
    {

        internal Session(byte[] id,ServerDetails stardogServerDetails)
        {
            Id = id;
            StardogServerDetails = stardogServerDetails;
        }

        public readonly ServerDetails StardogServerDetails;
        public readonly byte[] Id;  
        //Other non persistent session related data could go here. 
  
        public bool IsValid
        {
            get
            {
                return TokenStore.TokenIsValid(Id);
            }
        }


    }
}
