using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    public class byteComp : IEqualityComparer<byte[]>
    {
        #region IEqualityComparer<byte[]> Members

        public bool Equals(byte[] x, byte[] y)
        {
            if ((x == null) || (y == null))
                return false;

            int xcount = x.GetLength(0); 
            int ycount = x.GetLength(0);
            if (xcount != ycount)
                return false;

            for (int xPos = xcount - 1; xPos >= 0; xPos--)
            {
                if (y[xPos] != x[xPos])
                    return false;
            }

            return true;
        }

        public int GetHashCode(byte[] obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            int sum = 0;
            foreach (byte cur in obj)
            {
                sum += cur;
            }
            return sum;
        }

        #endregion
    }

    public class SessionStore
    {
        public readonly Dictionary<byte[], Session> Sessions;

        private SessionStore()
        {               
            Sessions = new Dictionary<byte[], Session>(new byteComp());                         
        }

        #region        singleton stuff
        private static SessionStore TheSessionStore;
        private static object theSessionStoreLock= new object();

        public static SessionStore GetTheSessionStore()
        {
            if (TheSessionStore != null)
                return TheSessionStore;
            lock (theSessionStoreLock)
            {
                if (TheSessionStore == null)//still, after getting the lock
                    TheSessionStore = new SessionStore();                
            }
            return TheSessionStore;
        }
        #endregion

        #region private implementations
        private void createAndAddSession(byte[] id, ServerDetails stardogServer)
        {
            Session toAdd = new Session(id, stardogServer);
            Sessions.Add(toAdd.Id,toAdd);
        }

        private bool tryGetValidSession(byte[] id,out Session theSession)
        { 
            
            if(Sessions.TryGetValue(id,out theSession))            
                if (theSession.IsValid)
                    return true;
            if (theSession != null)// exists but is invalid, probably timed out. Stop storing it.            
                Sessions.Remove(id);
            
            theSession = null;//ensure that invalid sessions do not get returned
            return false;
        }

        #endregion

        #region public static accessors
        /// <summary>
        /// Attempts to get a valid session
        /// </summary>
        /// <param name="id">The session id - an array of 8 bytes</param>
        /// <param name="theSession">If successful the session object is passed out here. If not found, or not valid, passes null</param>
        /// <returns>True if found and valid otherwise false. DO NOT use theSession if false - it will be null</returns>
        public static bool TryGetValidSession(byte[] id, out Session theSession)
        {
            return GetTheSessionStore().tryGetValidSession(id, out theSession);
        }

        /// <summary>
        /// Creates a new session with the given id and data, STORED IN MEMORY ONLY 
        /// </summary>
        /// <param name="id">The ID (as used by the tokens) of the session</param>
        /// <param name="stardogServer">The stardog server details linked to this session</param>
        /// <remarks>
        /// If any other session data is added it should also be added to the parameter list
        /// </remarks>
        public static void CreateAndAddSession(byte[] id, ServerDetails stardogServer)
        {
             GetTheSessionStore().createAndAddSession(id, stardogServer);
        }
        #endregion
    }
}
