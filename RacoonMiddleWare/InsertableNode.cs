using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using MiddleWareBussinessObjects;
using System.Runtime.Serialization;
using MiddleWareBussinessObjects.LDLFileBO;
using MiddlewareConnectivity;

namespace RacoonMiddleWare
{
    [DataContract]
    public class InsertableNode
    {
        public InsertableNode()
        { }

        public InsertableNode(BONode toClone)
        {
            this.Value = toClone.Value;
            this.AddLanguageTagToString = toClone.AddLanguageIfString;
        }

        [DataMember]
        public object Value;
        [DataMember]
        public bool AddLanguageTagToString;

        /// <summary>
        /// Returns the value formatted for insertion into a sparl Query
        /// </summary>
        /// <param name="currentSession">An active session, used for obtaining the current language</param>
        /// <returns>a string which may be used directly in a sparql query</returns>
        public string ToSparqlString(Session currentSession)
        {
            Uri uriVal = Value as Uri;
            if (uriVal != null)            
                return string.Format(@"<{0}>", uriVal.OriginalString);            
            
            string strVal = Value as string;
            if (!string.IsNullOrEmpty(strVal))
            {
                if (AddLanguageTagToString)
                {
                    if (!string.IsNullOrEmpty(currentSession.Language))
                        return string.Format("\"{0}\"@{1}", strVal, currentSession.Language);
                }
                return string.Format("\"{0}\"", strVal);
            }

            if (Value.GetType() == typeof(DateTime))
            {
                string result = ((DateTime)Value).ToString("o");
                return string.Format("\"{0}\"^^xsd:dateTime", result);
            }

            if (Value.GetType() == typeof(bool))
            {                
                if((bool)Value)
                    return string.Format("\"true\"^^xsd:boolean");
                else
                    return string.Format("\"false\"^^xsd:boolean");
            }

            if (Value.GetType() == typeof(LDLGeoPoint))
            {
                return ((LDLGeoPoint)Value).ToSparql();
            }

            return Value.ToString();//numbers are fine as is
        }

        //really just for debugging, change if needed
        public override string ToString()
        {
            if (Value != null)
                return "Value:" + Value.ToString();
            else
                return "No Value";
        }


        
    }
}