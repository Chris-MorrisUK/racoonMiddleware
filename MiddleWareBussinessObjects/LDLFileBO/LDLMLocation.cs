using MiddlewareConnectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLMLocation : LDLBOBase
    {
        public const string Identifier = "M_LOCATION";
        public const string DistanceName = "DISTANCE";
        public const char OpenBracket = '(';
        public const char CloseBracket = ')';

        public string TrackStr;
        public LDLTrack Track;
        public decimal Distance;


        /// <summary>
        /// Only use me if you're calling Populate later on - inheritors do for easier parsing
        /// </summary>
        protected LDLMLocation():base(string.Empty)
        { 
        }
        /// <summary>
        /// NB I don't expect the entire line, just the definition of the MLocations
        /// </summary>
        /// <param name="def"></param>
        public LDLMLocation(string def):base(string.Empty)
        {
           PopulateLocationFields(def);
        }

        public static LDLMLocation CreateFromCompleteLine(string line)
        {
            LDLMLocation result = new LDLMLocation();
            int start = line.IndexOf(OpenBracket);
            int end = line.IndexOf(CloseBracket);
            string def = line.Substring(start+1, (end - start)-1);
            string[] parts = def.Split(LDLSeperators.MEASUREMENT_SEPERATOR);
            result.TrackStr = parts[0].Replace("\"","").Replace("(","").Trim();
            result.Distance = decimal.Parse(parts[1].Trim());
            return result;
        }

        protected void PopulateLocationFields(string def)
        {
            int sectionStartIdx = def.IndexOf(LDLSeperators.SUB_SECTION_START);
            int sectionEndIdx = def.IndexOf(LDLSeperators.SUB_SECTION_END, sectionStartIdx);
            def = def.Substring(sectionStartIdx + 1, (sectionEndIdx - sectionStartIdx) - 1);
            string[] parts = def.Split(LDLSeperators.MEASUREMENT_SEPERATOR);
            TrackStr = parts[0].Trim().Replace("\"", "");
            Distance = decimal.Parse(parts[1].Trim());            
        }

        public override void DoSecondPass(Dictionary<string, LDLBOBase> parsedObjects)
        {
            LDLBOBase trackObject;
            parsedObjects.TryGetValue(TrackStr, out trackObject);
            Track = trackObject as LDLTrack;
            this.ID = Track.ID + this.Distance.ToString();
        }

        protected override IEnumerable<BOTripple> GetCustomTripples()
        {
            List<BOTripple> customTripples = new List<BOTripple>();

            customTripples.Add(new BOTripple(this.AsNode, new BONode(LDLUris.LocatedOnProperty), Track.AsNode));
            customTripples.Add(BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.MeasurementValueProperty, this.Distance));
            customTripples.Add(BOTripple.CreateTrippleFromValues(this.AsNode, LDLUris.UnitProperty, LDLUris.Metre));

            return customTripples;
        }


        public override Uri TypeUri
        {
            get
            {
                return LDLUris.OffsetLocationType;
            }
        }

        public override string ObjectBaseUriStr
        {
            get
            {
                return LDLUris.OffsetLocationStr;
            }
        }
    }
}
