using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MiddleWareBussinessObjects
{

    public abstract class MiddlewareParameter
    {
        public string ParamName{ get ;  set; }
    }

   /// <summary>
   /// This is the copy of the parameter separate from the data contract, 
   /// such that the data contract can be updated without breaking the implementation. Also, this lets me put the implementation in a separate dll
   /// </summary>
    public class MiddlewareParameter<valueType> : MiddlewareParameter where valueType : class
    {
		public MiddlewareParameter(MiddlewareParameterDirection dir)
		{
			direction = dir;
		}        

        public MiddlewareParameter(string name, valueType val, MiddlewareParameterDirection dir) 
        {
            ParamName = name;
            paramValue = val;
            direction = dir;
        }

        valueType paramValue = null;
       
        public valueType ParamValue
        {
            get { return paramValue; }
            set { paramValue = value; }
        }
        private MiddlewareParameterDirection direction;

        public MiddlewareParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }
    }

    public enum MiddlewareParameterDirection
    {
        In=0,
        Out=1,
        Both=2
    }

 
}