using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLTrackPair : LDLBOBase
    {
        private const char startChar = '(';
        private const char endChar = ')';
        private const char seperratorChar = ',';

        public LDLTrackPair(string definition):base(string.Empty)
        {
            int defStart = definition.IndexOf(startChar);
            int defEnd = definition.IndexOf(endChar);
            string containsTracks = definition.Substring(defStart + 1, defEnd - defStart - 1);
            string[] tracks = containsTracks.Split(seperratorChar);
            Track1Str = tracks[0].Trim().Replace("\"","");
            Track2Str = tracks[1].Trim().Replace("\"", ""); 

        }
        public LDLTrack Track1;
        public LDLTrack Track2;

        public string Track1Str;
        public string Track2Str;


        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase trackObject;
            parsedObjects.TryGetValue(Track1Str, out trackObject);
            Track1 = trackObject as LDLTrack;
            parsedObjects.TryGetValue(Track2Str, out trackObject);
            Track2 = trackObject as LDLTrack;
        }
    }
}
