using System;
using System.Collections.Generic;
using MiddleWareBussinessObjects;
using System.Runtime.Serialization;
using System.Web;

namespace RacoonMiddleWare
{
    [DataContract(IsReference = true)]
    public class StringParameter : ParameterBase, IConvertToMiddlewareParam
    {
        public StringParameter(string name, string value, ParameterDirection _direction = ParameterDirection.Both)
            : base(name, _direction)
        {
            paramValue = value;
            IsUri = false;
            base.ValueType = typeof(string);
        }

        string paramValue = string.Empty;
        [DataMember]
        public string ParamValue
        {
            get { return paramValue; }
            set { paramValue = value; }
        }

        [DataMember]
        public bool IsUri;


        #region IConvertToMiddlewareParam Members

        public override MiddlewareParameter ToMiddlewareParam()
        {
            MiddlewareParameterDirection mwDirection = (MiddlewareParameterDirection)(int)Direction;
            if (!IsUri)
                return new MiddlewareParameter<string>(base.ParameterName, paramValue, mwDirection);
            else
                return getAsUri(mwDirection);

        }

        private  MiddlewareParameter<Uri> getAsUri(MiddlewareParameterDirection direction)
        {
            UriBuilder build = new UriBuilder(paramValue);
            return new MiddlewareParameter<Uri>(base.ParameterName, build.Uri, direction);
        }

        #endregion
    }
}