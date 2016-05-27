using System;
using System.Collections.Generic;
using RacoonMiddleWare;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace StoredProcCreator
{
    public class StoredProcList
    {
        private List<StoredProcedure> spStore;
        private static StoredProcList theStoredProcList;
        private static object spListLock = new object();

        private StoredProcList()
        {
            spStore = new List<StoredProcedure>();
        }

        public static StoredProcList GetSPList()
        {
            lock (spListLock)
            {
                if (theStoredProcList != null)
                    return theStoredProcList;
                else
                {
                    theStoredProcList = new StoredProcList();
                    return theStoredProcList;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {                
                return !spStore.Any();
            }
        }
        public void CreateAndAdd(string name, string type, string content,string server, string dataStore)
        {
            StoredProcedure toAdd = new StoredProcedure(name,type,content,server,dataStore);
            spStore.Add(toAdd);
        }

        /// <summary>
        /// Saves the list of stored procs to a settings file, suitably formatted for use by the middle-ware.
        /// </summary>
        /// <param name="fName">The path of the target file. The file must not exist</param>
        /// <returns>The number of records saved</returns>
        public int SaveToFile(string fName)
        {
            if (spStore == null)
                throw new InvalidOperationException("Tried to save list of stored procedures when list did not exist");
 
            XmlSerializer cereal = new XmlSerializer((typeof(List<StoredProcedure>)));
            if (File.Exists(fName))
            {
                using (FileStream fs = new FileStream(fName, FileMode.Open, FileAccess.Read))
                {
                    List<StoredProcedure> savedSprocs = cereal.Deserialize(fs) as List<StoredProcedure>;
                    if (savedSprocs == null)
                        throw new ArgumentException("Failed to parse settings file", fName);
                    spStore.AddRange(savedSprocs);
                }
            }

            using (FileStream fs = new FileStream(fName, FileMode.Create, FileAccess.Write))
            {
                
                cereal.Serialize(fs, spStore);
                return spStore.Count;
            }            
        }

    }
}
