#Racoon Middleware Webservices
##Purpose
The purpose of this middleware is to allow integration between the Railway Core Ontologies and bulk data stores such as REDIS. Common functions can be automated and their processing time reduced by means of stored procedures.
##Usage Overview
Since REDIS lacks an inbuilt security mechanism authentication is handled by the middleware. Usernames and password hashes (generated using the blow fish algorism) are held in REDIS. Note that it is expected that the administrator will configure the network such that the REDIS server is accessible to Racoon Middleware, but not external hosts. Both credentials and server details for stardog are also stored on your behalf, these credentials and server details can be varied for each stored procedure, otherwise those that are supplied at authentication time are used. If only stored procedures with attached server details are to be used then none need be supplied at authentication time. 
Stored procedures are created at setup time using the supplied tool.

##Authentication
In order as to use the web services it is necessary for users to be authenticated.
The process is as outlined below.
 
Authentication is achieved via the Authenticate web service defined below.

 ```C#
 [OperationContract]
RacoonAurthorisationResponse Authenticate(string userName, string password, string stardogUser, string stardogPassword, Uri stardogServer, string stardogDatastore); 
```

The connection must be configured to use SSL otherwise password will be sent in plain text. The response will be of type RacoonAurthorisationResponse.  The base response class is discussed in Responses, additionally a byte[] Token is returned. This is an array of eight bytes which you should keep for use with all other services. If the request is denied then null will be returned.  It is currently valid for 20 minutes, after which time you must request another.

##Responses

All responses from the middleware extend  SimpleRacoonResponse, which is outlined in more detail below. You will note that this in turn implements IRacoonResponse, thus all responses from racoon implement IRacoonResponse.
 ```C#
[DataContract]
    public class SimpleRacoonResponse : IRacoonResponse
    {
        [DataMember]
        public bool Status;       

        [DataMember]
        public Exception Error;      
        
        [DataMember]    
        public bool AuthorisationOK;        
    }
}
 ``` 


This information (at the very least) is provided from every webservice.
Status: indicates whether the request succeeded. If it is false then the operation did not go ahead.
Error:  Gives more detail on the cause of any problem.
AuthorisationOK:  If this is false Authorisation was unsuccessful – the token supplied was expired or invalid.
Each service then has a response that provides extra information as required, or returns a SimpleRacoonResponse in the case that no extra information need be provided.


