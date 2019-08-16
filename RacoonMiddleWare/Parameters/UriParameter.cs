using System;
using System.Collections.Generic;
using MiddleWareBussinessObjects;
using System.Web;
using System.Runtime.Serialization;

namespace RacoonMiddleWare
{
    [DataContract(IsReference = true)]
    public class UriParameter : ParameterBase, IConvertToMiddlewareParam
    {

        public UriParameter(string name, Uri value, ParameterDirection _direction = ParameterDirection.Both)
            : base(name, _direction)
        {
            paramValue = value;
            base.ValueType = typeof(Uri);
        }

        private Uri paramValue;

        [DataMember]
        public Uri ParamValue
        {
            get { return this.paramValue; }
            set { this.paramValue = value; }
        }



        #region IConvertToMiddlewareParam Members

        public override MiddlewareParameter ToMiddlewareParam()
        {
            MiddlewareParameterDirection mwDirection = (MiddlewareParameterDirection)(int)Direction;
            return new MiddlewareParameter<Uri>(base.ParameterName, paramValue, mwDirection);
        }

        #endregion
    }
}