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
        }



        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            return base.GetCustomTripples();
        }

    }
}
