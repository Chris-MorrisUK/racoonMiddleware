using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using MiddleWareBussinessObjects;

namespace RacoonMiddleWare
{
   
    public class StoredProcedure
    {
        public StoredProcedure()
        {
            theQueryLock = new object();
        }
        public StoredProcedure(string name, string typeOfQuerry, string storedProcText,string server,string datastore):this()
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentOutOfRangeException("You must supply a name", "name");
            Name = name;
            TypeOfQuerry = typeOfQuerry;
            StoredProcText = storedProcText;
            Server = server;
            DataStore = datastore;
            KeyHash = name.GetHashCode();
            createQuery();
        }
        
        
        private void createQuery()
        {
            lock (theQueryLock)
            {                
                //Cause the exception to be thrown here if the type doesn't exist, for clarity sake.
                Type queryType = Type.GetType(TypeOfQuerry, true);
                TheQuerry = Activator.CreateInstance(queryType) as IQuerry;
                if (TheQuerry == null)
                    throw new InvalidOperationException("The Query is not of a valid type");
                TheQuerry.SetTarget(Server, DataStore);
                TheQuerry.SetQuerry(StoredProcText);
            }
        }

        private object theQueryLock;
        private IQuerry theQuery;

        /// <summary>
        /// A hash of the storedproc name, used as a key to retrieve it
        /// </summary>
        public int KeyHash;

        /// <summary>
        /// The executable version of the query
        /// </summary>
        [XmlIgnore]
        public IQuerry TheQuerry
        {
            get
            {
                if (theQuery == null) createQuery();
                return theQuery;
            }
            private set
            {
                theQuery = value;
            }
        }

        /// <summary>
        /// The server at which the stored proc is targeted. Where this is null or empty the value from the Session is used
        /// </summary>
        public string Server;
        /// <summary>
        /// The Datastore at which the stored proc is targeted. Where this is null or empty the value from the Session is used
        /// </summary>
        public string DataStore;
        /// <summary>
        /// The name of stored proc
        /// </summary>
        public string Name;
        /// <summary>
        /// This is the name of the type of query to instantiate 
        /// </summary>
        public string TypeOfQuerry;
        /// <summary>
        /// The text of the command e.g. SELECT * WHERE {?s ?p ?o}
        /// </summary>
        public string StoredProcText;
    }
}