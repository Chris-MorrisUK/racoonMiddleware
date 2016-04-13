using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using REDISConnector;

namespace MiddleWareBussinessObjects
{
    /// <summary>
    /// So: this is the most basic user, with only the details held in REDIS. 
    /// Fuller details can and should be held in the ontology - an extension of this class is envisaged.
    /// </summary>
    public class UserBase: IRedisAccessable
    {
        public UserBase()
        { }

        public UserBase(string name)
        {
            UserName = name;
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public void SetPassword(string password)
        {
            Hash = BCrypt.Net.BCrypt.GenerateSalt();
            HashedPassword = BCrypt.Net.BCrypt.HashPassword(password, Hash);
            bool success = BCrypt.Net.BCrypt.Verify(password, HashedPassword);
        }


        private string hashedPassword;

        public string HashedPassword
        {
            get { return hashedPassword; }
            set { hashedPassword = value; }
        }

        private string hash;

        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }

        private Uri universalID;

        public Uri UniversalID
        {
            get { return universalID; }
            set { universalID = value; }
        }

        bool isVald = true;

        public virtual bool IsVald
        {
            get { return isVald; }
            set { isVald = value; }
        }

        public string RedisKey
        {
            get { return this.userName + REDISKEYSUFFIX; }
        }
        public static readonly string REDISKEYSUFFIX = ":userObject";
    }
}
