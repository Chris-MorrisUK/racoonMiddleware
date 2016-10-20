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

        public LDLMSection(string defition):
            base(string.Empty)//no id
        {
            string[] parts = defition.Split(sectionDelimiter);
            TrackAsStr = parts[0].Trim();
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
    }
}
