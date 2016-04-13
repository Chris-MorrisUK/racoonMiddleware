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
        { }
        public StoredProcedure(string name, string typeOfQuerry, string storedProcText)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentOutOfRangeException("You must supply a name", "name");
            Name = name;
            TypeOfQuerry = typeOfQuerry;
            StoredProcText = storedProcText;
            KeyHash = name.GetHashCode();
           // createQuery();
        }

        private void createQuery()
        {
            
           // Console.WriteLine(typeof(StardogConnection.StardogQuery).AssemblyQualifiedName);
            Type queryType = Type.GetType(TypeOfQuerry);
            TheQuerry = Activator.CreateInstance(queryType) as IQuerry;
            TheQuerry.SetQuerry(StoredProcText);
        }

        /// <summary>
        /// A hash of the storedproc name, used as a key to retrieve it
        /// </summary>
        public int KeyHash;
        [XmlIgnore]
        public IQuerry TheQuerry;
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