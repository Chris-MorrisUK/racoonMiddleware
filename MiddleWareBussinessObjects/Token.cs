using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Web;
using REDISConnector;

namespace MiddleWareBussinessObjects 
{
    [DataContract]
    public class Token : IRedisAccessable
    {
        public Token()
        {
            id = new byte[8];
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(id);
        }

        public Token(string _userName)
            : this()
        {
            UserName = _userName;
        }


        public Token(UserBase _user,bool _valid):this()
        {
            userName = _user.UserName;
            stardogUserUri = _user.UniversalID;
            valid = _valid;
        }

        private byte[] id;

        [DataMember]
        public byte[] Id
        {
            get { return id; }
            set { id = value; }
        }

        private bool valid;

        [DataMember]
        public bool Valid
        {
            get { return valid; }
            set { valid = value; }
        }

        private string userName;

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private Uri stardogUserUri;

        [DataMember]
        public Uri StardogUserUri
        {
            get { return stardogUserUri; }
            set { stardogUserUri = value; }
        }

        #region IRedisAccessable Members

        public string RedisKey
        {
            get { return RedisKeyFromID(Id); }
        }
        
        public static readonly string REDISKEYSUFFIX = Properties.Settings.Default.userNameToToken;

        #endregion

        public static string RedisKeyFromID(byte[] ID)
        {
            return System.Text.Encoding.Unicode.GetString(ID) + REDISKEYSUFFIX;
        }
    }
}