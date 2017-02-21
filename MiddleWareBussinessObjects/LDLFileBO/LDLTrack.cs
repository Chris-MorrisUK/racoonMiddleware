using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrack : LDLBOBase, ILDL_GPSLocated
    {
        public string Node0AsStr;
        public string Node1AsStr;
        public LDLBOBase Node0;
        public LDLBOBase Node1;
        public decimal Length;
       
        public LDLGraphicLine Graphic_Line;

        public LDLTrack(string id):base(id)
        {
            locations = new List<LDLGPSPoint>();
        }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {    
            if(!string.IsNullOrEmpty(Node0AsStr))
                parsedObjects.TryGetValue(Node0AsStr, out Node0);
            if (!string.IsNullOrEmpty(Node1AsStr))
                parsedObjects.TryGetValue(Node1AsStr, out Node1);
             
        }


        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> result = new List<BOTripple>();
            if (Length > 0)
            {
                BOTripple lengthTripple = new BOTripple();
                lengthTripple.Subject = AsNode;
                lengthTripple.Predicate = new BONode(LDLUris.LengthProperty);
                lengthTripple.Object = new BONode(Length);
                result.Add(lengthTripple);
            }

            if (Node0 != null)
            {
                BOTripple Node0LinkTripple = new BOTripple();
                Node0LinkTripple.Subject = AsNode;
                Node0LinkTripple.Predicate = new BONode(LDLUris.nodeProperty);
                Node0LinkTripple.Object = Node0.AsNode;
                result.Add(Node0LinkTripple);
            }

            if (Node1 != null)
            {
                BOTripple Node1LinkTripple = new BOTripple();
                Node1LinkTripple.Subject = AsNode;
                Node1LinkTripple.Predicate = new BONode(LDLUris.nodeProperty);
                Node1LinkTripple.Object = Node1.AsNode;
                result.Add(Node1LinkTripple);
            }
            if (locations.Count > 0)
            {
                foreach (LDLGPSPoint location in locations)                
                    result.AddRange(location.GetTripples(ID,this.AsNode));
                
            }

            return result;
        }

        

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.TrackClassString;
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.TrackClassUri;
            }
        }

        private List<LDLGPSPoint> locations;
        public List<LDLGPSPoint> Locations
        {
            get { return locations; }
        }
    }
}
