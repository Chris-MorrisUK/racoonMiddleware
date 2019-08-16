using System;
using System.Collections.Generic;
using MiddleWareBussinessObjects;
using System.Web;
using System.Runtime.Serialization;

namespace RacoonMiddleWare
{    
    [DataContract(IsReference = true)]
    public class ByteParameter : ParameterBase, IConvertToMiddlewareParam
    {

        public ByteParameter(string name, byte[] value, ParameterDirection _direction = ParameterDirection.Both)
            : base(name, _direction)
        {
            paramValue = value;
            base.ValueType = typeof(byte[]);
        }

        private byte[] paramValue;

        [DataMember]
        public byte[] ParamValue
        {
            get { return this.paramValue; }
            set { this.paramValue = value; }
        }



        #region IConvertToMiddlewareParam Members

        public override MiddlewareParameter ToMiddlewareParam()
        {
            MiddlewareParameterDirection mwDirection = (MiddlewareParameterDirection)(int)Direction;
            return new MiddlewareParameter<byte[]>(base.ParameterName, paramValue, mwDirection);
        }

        #endregion
    }
}