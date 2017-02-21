using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLBOBase
    {
        public LDLBOBase(string id)
        {
            ID = id;
            //if (id.Length < 2)
            //    ID = string.Empty;
           // else
              //  ID = id.Substring(1, id.Length - 2);
        }
        public string ID { get; protected set; }

        public virtual void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        { }

        public virtual string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.IndependantThing;
            }
        }

        /// <summary>
        /// This is can be overridden, but shouldn't be in normal circumstances
        /// </summary>
        public virtual Uri ObjectUri
        {
            get
            {
                return new Uri(ObjectBaseUriStr + "/" + ID.Replace("\"",""));
            }
        }

        public virtual BONode AsNode
        {
            get
            {
                return new BONode(ObjectUri);
            }
        }

        /// <summary>
        /// Override this in every Business object
        /// </summary>
        public virtual Uri TypeUri
        {
            get
            {
                return new Uri(ObjectBaseUriStr);
            }
        }

        /// <summary>
        /// Gets a representation of the business object that can be inserted into a triple store 
        /// </summary>
        /// <remarks>Not to be over ridden in normal circumstances</remarks>
        /// <returns></returns>
        public IEnumerable<BOTripple> GetAsTripples()
        {
            List<BOTripple> results = new List<BOTripple>();
            if (!string.IsNullOrEmpty(ID))
            {
                BOTripple idTripple = new BOTripple();
                idTripple.Subject = new BONode(ObjectUri);
                idTripple.Predicate = new BONode(LDLUris.ItemIDUri);
                idTripple.Object = new BONode(ID);
                results.Add(idTripple);
            }
            results.AddRange(GetCustomTripples());
            results.Add(GetTypeTripple());
            return results;
        }

        protected virtual BOTripple GetTypeTripple()
        {
            BOTripple typeTripple = new BOTripple();
            typeTripple.Subject = this.AsNode;
            typeTripple.Predicate = new BONode(LDLUris.RDFType);
            typeTripple.Object = new BONode(TypeUri);
            return typeTripple;
        }

        protected virtual IEnumerable<BOTripple> GetCustomTripples()
        {
            return Enumerable.Empty<BOTripple>();
        }

        
    }
}
