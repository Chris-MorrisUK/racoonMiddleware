using MiddleWareBussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;

namespace RacoonMiddleWare
{
    [DataContract]
    public class InsertableTriple
    {
        public InsertableTriple()
        { }

        public InsertableTriple(BOTripple toClone)
        {
            this.Subject = new InsertableNode(toClone.Subject);
            this.Predicate = new InsertableNode(toClone.Predicate);
            this.Object = new InsertableNode(toClone.Object);
        }

        [DataMember]
        public InsertableNode Subject;
        [DataMember]
        public InsertableNode Predicate;
        [DataMember]
        public InsertableNode Object;

        public string ToSparqlLine(Session current)
        {
            try
            {
                StringBuilder result = new StringBuilder();
                result.Append(Subject.ToSparqlString(current));
                result.Append(" ");
                result.Append(Predicate.ToSparqlString(current));
                result.Append(" ");
                result.Append(Object.ToSparqlString(current));
                result.AppendLine(" .");
                return result.ToString();
            }
            catch (NullReferenceException )
            {
                return string.Empty;
            }
        }
    }
}