using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace RacoonMiddleWare
{
    
    public interface IRacoonResponse
    {
  
        bool Status { get; set; }
        Exception Error { get; set; }
        bool AuthorisationOK { get; set; }
    }
}