using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Runtime.Serialization.Json;
using System.IO;

namespace REDISConnector
{
    public class REDISConnector
    {
        private static ConnectionMultiplexer redis;
        private static object redisLock = new object();

        /// <summary>
        /// You should hold onto the result of this, rather than keep waiting for this to get a lock
        /// </summary>
        /// <returns></returns>
        public static ConnectionMultiplexer GetRedis()
        {
            if (redis != null) return redis;
            lock (redisLock)
            {
                if (redis == null)
                {
                    redis = ConnectionMultiplexer.Connect(Properties.Settings.Default.REDISHost);                    
                }
                return redis;
            }
        }        

        public static  string GetValue(string key)
        {
            IDatabase redisDB = getRedisDB();
            string result =  redisDB.StringGet(key);
            return result;
        }
        public static byte[] GetValueBytes(string key)
        {
            IDatabase redisDB = getRedisDB();
            byte[] result = redisDB.StringGet(key);
            return result;
        }

        public static bool SetValue(string key, string value)
        {
            IDatabase redisDB = getRedisDB();
            return redisDB.StringSet(key, value);
        }

        public static byte[] GetValue(byte[] key)
        {
            IDatabase redisDB = getRedisDB();
            RedisValue result = redisDB.StringGet(key);
            return result;
        }

        public static bool SetValue(byte[] key, byte[] value)
        {
            IDatabase redisDB = getRedisDB();
            return redisDB.StringSet(key, value);
        }

        public static bool SetValue(string key, byte[] value,TimeSpan timeout)
        {
            IDatabase redisDB = getRedisDB();            
            return redisDB.StringSet(key, value, timeout);
        }


        private static IDatabase getRedisDB()
        {
            if (redis == null) GetRedis();
            IDatabase redisDB = redis.GetDatabase();
            return redisDB;
        }


        public static bool SerializeAndSetValue(string key, object value, TimeSpan? timeout = null)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(value.GetType());
            IDatabase redisDB = getRedisDB();
            bool result = false;
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, value);
                result = redisDB.StringSet(key, ms.ToArray(), timeout);
            }
            return result;
        }

        public static bool SerializeAndSetValue(IRedisAccessable value, TimeSpan? timeout = null)
        {
            return SerializeAndSetValue(value.RedisKey, value, timeout);
        }

        public static TReturn GetDeserializedValue<TReturn>(string key) where TReturn : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TReturn));
            
            IDatabase redisDB = getRedisDB();
            byte[] found = redisDB.StringGet(key);
            if ((found == null)||(found.LongLength == 0))
                return null;
            object result =null;
            using (MemoryStream ms = new MemoryStream(found))            
                result = serializer.ReadObject(ms);
            
            return result as TReturn;
        }

        public static bool CheckForExistance(string key)
        {
            IDatabase redisDB = getRedisDB();
            return redisDB.KeyExists(key);
        }
    }
}
