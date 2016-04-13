using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REDISConnector;

namespace UserManager
{
    public class UserStore
    {
        private static UserStore theUserStore;
        private static object theUserStoreLock = new object();

        private UserStore()
        {
            
        }

        private static UserStore getTheUserStore()
        {
            if (theUserStore != null)
                return theUserStore;
            lock (theUserStoreLock)
            {
                theUserStore = new UserStore();
                return theUserStore;
            }
        }

        private bool checkIfUserExists(string userName)
        {
            if (userName.Contains(":"))//it's bit of an edge case, but it could act like a sql injection attack - username:othervar may exist when username doesn't
                throw new ArgumentException("Usernames do not contain a :.", userName);           
            return REDISConnector.REDISConnector.CheckForExistance(userName);
        }

        public static bool CheckIfUserExists(string userName)
        {
            return getTheUserStore().checkIfUserExists(userName);
        }
    }
}
