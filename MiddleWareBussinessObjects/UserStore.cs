using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using REDISConnector;
using MiddleWareBussinessObjects;

namespace MiddleWareBussinessObjects
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

        private bool addUser(UserBase toAdd)
        {
            return REDISConnector.REDISConnector.SerializeAndSetValue(toAdd.RedisKey, toAdd);
        }

        private bool checkIfUserExists(string userName)
        {
            if (userName.Contains(":"))//it's bit of an edge case, but it could act like a sql injection attack - username:othervar may exist when username doesn't
                throw new ArgumentException("Usernames do not contain a :.", userName);
            return REDISConnector.REDISConnector.CheckForExistance(getREDISKey(userName));
        }

        private UserBase getUser(string name)
        {
            return REDISConnector.REDISConnector.GetDeserializedValue<UserBase>(getREDISKey(name));
        }

        public static bool CheckIfUserExists(string userName)
        {
            return getTheUserStore().checkIfUserExists(userName);
        }

        public static bool AddUser(UserBase toAdd)
        {
            return getTheUserStore().addUser(toAdd);
        }

        public static UserBase GetUser(string name)
        {
            return getTheUserStore().getUser(name);
        }

        private static string getREDISKey(string name)
        {
            return name + UserBase.REDISKEYSUFFIX;
        }
    }
}
