using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace RacoonMiddleWare
{
    public class StoredProcStore
    {
        private Dictionary<int, StoredProcedure> availableStoredProcudes;
        private static StoredProcStore theStoredProcStore;
        private static object theStoredProcStoreLock = new object();

        private StoredProcStore(string settingsFile)
        {
            XmlSerializer cereal = new XmlSerializer((typeof(List<StoredProcedure>)));

            using (FileStream fs = new FileStream(settingsFile, FileMode.Open, FileAccess.Read))
            {
                List<StoredProcedure> savedSprocs = cereal.Deserialize(fs) as List<StoredProcedure>;
                if (savedSprocs == null)
                    throw new ArgumentException("Failed to parse settings file", settingsFile);
                availableStoredProcudes = new Dictionary<int, StoredProcedure>();
                foreach (StoredProcedure sp in savedSprocs)
                {
                    availableStoredProcudes.Add(sp.KeyHash, sp);
                }
            }
        }




        public static StoredProcStore TheStoredProcStore
        {
            get
            {
                lock (theStoredProcStoreLock)
                {
                    if (theStoredProcStore == null)
                        theStoredProcStore = new StoredProcStore(Properties.Settings.Default.QueryDefsPath);
                    return theStoredProcStore;
                }
                
            }
        }

        public bool TryGetSproc(int key,out StoredProcedure Result)
        {
            if (!availableStoredProcudes.TryGetValue(key, out Result))
                return false;
            if (Result == null)
                return false;
            return true;
        }
    }
}