using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    public class TokenStore
    {
        public static byte[] CreateAndAddToken(UserBase aurthorisedUser)
        {
            Token token = new Token(aurthorisedUser,true);
            REDISConnector.REDISConnector.SerializeAndSetValue(token, tokenValidity);
            return token.Id;
        }

        public static bool TokenIsValid(byte[] id)
        {
            return REDISConnector.REDISConnector.CheckForExistance(Token.RedisKeyFromID(id));
        }

        private static TimeSpan tokenValidity
        {
            get
            {
                return TimeSpan.FromMinutes(MINUTESVALID);
            }
        }

		public readonly static int MINUTESVALID = 60;
    }

}
