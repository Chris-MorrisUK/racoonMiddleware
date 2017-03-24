using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLMSection : LDLBOBase
    {

        
        private const char sectionDelimiter = ',';

        public string TrackAsStr;
        public LDLTrack Track;
        public float Start;
        public float End;

        protected LDLMSection() :
            base(string.Empty)//no id
        {           
        }

        public LDLMSection(string defition):
            base(string.Empty)//no id
        {
            this.Populate(defition);
        }

        protected void Populate(string defition)
        {             
            string[] parts = defition.Split(sectionDelimiter);
            TrackAsStr = parts[0].Trim().Replace("\"", "");
            string startStr = parts[1].Trim();
            Start = float.Parse(startStr);
            string endStr = parts[2].Trim();
            End = float.Parse(endStr);            
        }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase trackObject;
            parsedObjects.TryGetValue(TrackAsStr, out trackObject);
            Track = trackObject as LDLTrack;
            this.ID = Track.ID + "_" + Start.ToString() + "_" + End.ToString();//I want this to have an ID and be saved as a node in the ontology
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.MetersLocationClass;            
            }
        }

        public override Uri TypeUri
        {
            get
            {
                return LDLUris.MetersLocationClassUri;
            }
        }
        


        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> customTripples = new List<BOTripple>();
            if(Track != null)
                customTripples.Add(new BOTripple(this.AsNode, new BONode(LDLUris.LocatedOnProperty), this.Track.AsNode));
            customTripples.Add(BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.StartTrackPositionProperty, this.Start));
            customTripples.Add(BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.EndTrackPositionProperty, this.End));

            return customTripples;
        }

    }
}
