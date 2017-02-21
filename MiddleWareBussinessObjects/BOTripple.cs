using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects
{
    public class BOTripple
    {
        public BOTripple()
        { 

        }

        
        public BOTripple(BONode subject, BONode predicate, BONode _object)
        {
            Subject = subject;
            Predicate = predicate;
            Object = _object;
        }
        public BONode Subject;
        public BONode Predicate;
        public BONode Object;

        public override string ToString()
        {
            return "Subject: " + (Subject.Value.ToString() ?? "<null>")
                + " Predicate: " + ((Predicate.Value.ToString() ?? "<null>"))
                + " Object: " + (Object.Value.ToString() ?? "<null>");
        }

        //Regularly need to create a triple from this combination of things - business objects supply a node as base, then the values for the other 2
        public static BOTripple CreateTrippleFromValues(BONode subject, object predicate, object _object)
        {
            return new BOTripple(subject, new BONode(predicate), new BONode(_object));
        }
    }
}
