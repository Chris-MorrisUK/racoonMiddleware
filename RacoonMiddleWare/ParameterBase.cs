using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using MiddleWareBussinessObjects;

namespace RacoonMiddleWare
{
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract(IsReference = true)]
    [KnownType(typeof(UriParameter))]
    [KnownType(typeof(StringParameter))]
    [KnownType(typeof(ByteParameter))]
	[KnownType(typeof(MultiParameterResult))]
    public class ParameterBase: IConvertToMiddlewareParam//, IParamBase
    {


        public ParameterBase()
        {
            ValueType = typeof(Object);
        }

        public ParameterBase(string name, ParameterDirection dir = ParameterDirection.Both)
        {
            parameterName = name;
            direction = dir;
        }

        string parameterName = string.Empty;
        

        [DataMember]
        public string ParameterName
        {
            get { return parameterName; }
            set { parameterName = value; }
        }

        private ParameterDirection direction;

        [DataMember]
        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }
        
        public Type ValueType {  get;  protected set; }


        #region IConvertToMiddlewareParam Members

        public virtual MiddlewareParameter ToMiddlewareParam()
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}