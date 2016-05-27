using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonMiddleWare
{
    [DataContract]
    public class SimpleRacoonResponse : IRacoonResponse
    {
		public void CloneToPopulate(IRacoonResponse toClone)
		{
			this.AuthorisationOK = toClone.AuthorisationOK;
			this.Error = toClone.Error;
			this.Status = toClone.Status;
		}

        #region IRacoonResponse Members
        bool status;
        [DataMember]
        public bool Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        Exception error;

        [DataMember]
        public Exception Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;
            }
        }

        private bool authorisationOK;

        [DataMember]    
        public bool AuthorisationOK
        {
            get
            {
                return authorisationOK;
            }
            set
            {
                authorisationOK = value;
            }
        }

        #endregion
    }
}