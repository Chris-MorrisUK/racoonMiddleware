using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    //Much like the system.windows Point but with less importing needed
    public class LDLPoint
    {
        private const char POINT_SEPERATOR = ',';
        public const string POINT_START = "(";
        public const string POINT_END = ")";
        public LDLPoint(string definition)
        {
            string[] coords = definition.Split(POINT_SEPERATOR);
            string xStr = coords[0].Replace("(", "").Replace(":","").Trim();
            string yStr = coords[1].Replace(")", "").Trim();
            X = int.Parse(xStr);
            Y = int.Parse(yStr);
        }
        public int X;
        public int Y;


        public IEnumerable<BOTripple> GetAsTripples(BONode locatedSubject)
        {
            List<BOTripple> toInsert = new List<BOTripple>();
            BONode xPredicate = new BONode(LDLUris.DiagramXProperty);
            BONode xObject = new BONode(X);
            toInsert.Add(new BOTripple(locatedSubject, xPredicate, xObject));

            BONode yPredicate = new BONode(LDLUris.DiagramYProperty);
            BONode yObject = new BONode(Y);
            toInsert.Add(new BOTripple(locatedSubject, yPredicate, yObject));

            return toInsert;
        }
    }
}
