using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLParser
    {
        private const string POS_LINE_MARKER = "GPS";
        public static LDLParser GetParser()
        {
            if (theParser == null)
            {
                lock (theParserLock)
                {
                    if (theParser == null)
                        theParser = new LDLParser();
                }
            }
            return theParser;
        }
        private static object theParserLock = new object();
        private static LDLParser theParser;
        private LDLParser()
        {
            
            sectionFactories = createParserList();
            //absoluteLocations = new List<ILDL_GPSLocated>();
        }

        Dictionary<string, LDLBOBase> parsedObjects;
        Dictionary<string, IParsableFactory> sectionFactories;
       // List<ILDL_GPSLocated> absoluteLocations;

        public void ParseText(string[] definition,string[] absolutePosData)
        {
            parsedObjects = new Dictionary<string, LDLBOBase>();
            int sectionStart = 0;
            int sectionEnd = 0;

            int nLines = definition.GetLength(0);

            while ((sectionStart = getIndexOfSectionStart(definition, sectionEnd, nLines)) > 0)
            {
                if ((sectionEnd = getIndexOfSectionEnd(definition, sectionStart, nLines)) < 0)
                    throw new ArgumentException("LDL File missing end tag");

                int sectionToCopyStart = sectionStart + 2;
                int sectionLength = (sectionEnd - sectionToCopyStart) - 2;//we need neither the title nor the braces at ethier end                
                string[] sectionLines = new String[sectionLength];
                Array.Copy(definition, sectionToCopyStart, sectionLines, 0, sectionLength);
                //level is in line 0,  but I don't believe I need it
                string sectionType, sectionID;
                parseSectionTypeAndID(out sectionType, out sectionID, definition[sectionStart]);
                createSection(sectionType, sectionID, sectionLines);
            }

            doSecondPass();
            doAbsolutePositionData(absolutePosData);
        }

        private void doAbsolutePositionData(string[] absolutePosData)
        {
            foreach (string line in absolutePosData)
            {
                processAbsLine(line);
            }
        }

        private void processAbsLine(string line)
        {
            
                string[] cols = line.Split(LDLSeperators.ABSOLUTE_DATA_COL_SEPERATOR);
                if (cols.GetUpperBound(0) < 4)
                    return;
                if (cols[0].Trim() != POS_LINE_MARKER)
                    return;
                string nodeStr = cols[1].Trim();
                string offsetStr = cols[2].Trim();
                string latStr = cols[3].Trim();
                string longStr = cols[4].Trim();
                double offset = Convert.ToDouble(offsetStr);
                float lat = float.Parse(latStr);
                float longitude = float.Parse(longStr);
                if ((lat == 0) && (longitude == 0))
                    return;//quite a lot of items in there without a position?
                ILDL_GPSLocated track = GetTrack(nodeStr);
                if (track == null)
                    return;
                LDLGPSPoint point = new LDLGPSPoint(lat, longitude, offset);
                track.Locations.Add(point);
                //absoluteLocations.Add(track);
           
        }

        private ILDL_GPSLocated GetTrack(string id)
        {
            LDLBOBase nodeAsBase;
            if (parsedObjects.TryGetValue(id,out nodeAsBase))
            {
                ILDL_GPSLocated node = nodeAsBase as ILDL_GPSLocated;
                return node;
            }
            return null;
        }

        public List<BOTripple> GetAsTripples()
        {
            List<BOTripple> tripples = new List<BOTripple>();
            foreach (LDLBOBase item in parsedObjects.Values)
                tripples.AddRange(item.GetAsTripples());
            return tripples;
        }

        private void doSecondPass()
        {
            foreach (LDLBOBase item in parsedObjects.Values)
                item.DoSecondPass(parsedObjects);
        }

        private static int getIndexOfSectionStart(string[] definition, int currentEnd, int nLines)
        {
            int indexOfStart = currentEnd;
            while (indexOfStart < nLines)
                if (definition[indexOfStart++].StartsWith(LDLSeperators.SECTION_START))
                    return indexOfStart;
            return -1;
        }

        private static int getIndexOfSectionEnd(string[] definition, int currentStart, int nLines)
        {
            int indexOfEnd = currentStart;
            while (indexOfEnd < nLines)
                if (definition[indexOfEnd++].StartsWith(LDLSeperators.SECTION_END))
                    return indexOfEnd;
            return -1;
        }

        private bool createSection(string sectionType, string sectionID, string[] sectionLines)
        {
            IParsableFactory sectionFactory;
            if (sectionFactories.TryGetValue(sectionType, out sectionFactory))
            {
                LDLBOBase parsed = sectionFactory.CreateItem(sectionLines, sectionID);
                parsedObjects.Add(parsed.ID, parsed);
                return true;
            }
            else
                return false;
        }

        private static void parseSectionTypeAndID(out string sectionType, out string sectionID, string typesAndIdString)
        {
            string[] parts = typesAndIdString.Split(new string[] { LDLSeperators.CLASS_ID_DIVIDE }, StringSplitOptions.RemoveEmptyEntries);
            sectionType = parts[0].Trim();
            sectionID = parts[1].Trim().Substring(1, parts[1].Length -3);//remove the speach marks early on
            sectionID = sectionID.Remove(sectionID.Length - 1);//remove the terminator
        }


        private static Dictionary<string, IParsableFactory> createParserList()
        {
            Dictionary<string, IParsableFactory> result = new Dictionary<string, IParsableFactory>();
            LDLTrackFactory trackFactory = new LDLTrackFactory();
            LDLTrackEndFactory trackEndFactory = new LDLTrackEndFactory();
            LDLSimpleNodeFactory nodeFactory = new LDLSimpleNodeFactory();
            LDLTrackBoundaryFactory trackBoundaryFactory = new LDLTrackBoundaryFactory();
            LDLSwitchedDimondFactory switchedFactory = new LDLSwitchedDimondFactory();
            LDLPointsFactory pointsFact = new LDLPointsFactory();
            LDLDimondFactory dimondFact = new LDLDimondFactory();
            LDLInterlockingFactory interlockingFact = new LDLInterlockingFactory();
            LDLSignalFactory signalFact = new LDLSignalFactory();
            LDLNormalApproachTriggerFactory normalApprachFactory = new LDLNormalApproachTriggerFactory();
            LDLReverseApproachTriggerFactory reverseApproachFactory = new LDLReverseApproachTriggerFactory();
            LDLTrackCircuitFactory tcFact = new LDLTrackCircuitFactory();
            TrackCiruitJoinFactory tcJoinFact = new TrackCiruitJoinFactory();
            LDLBufferStopFactory buffStopFact = new LDLBufferStopFactory();
            LDLRouteFactory routeFact = new LDLRouteFactory();
            LDLBaliseFactory baliseFact = new LDLBaliseFactory();
            LDLBaliseGroupFactory baliseGroupFact = new LDLBaliseGroupFactory();
            result.Add(baliseGroupFact.Identifier, baliseGroupFact);
            result.Add(baliseFact.Identifier, baliseFact);
            result.Add(routeFact.Identifier, routeFact);
            result.Add(buffStopFact.Identifier, buffStopFact);
            result.Add(tcJoinFact.Identifier, tcJoinFact);
            result.Add(tcFact.Identifier, tcFact);
            result.Add(reverseApproachFactory.Identifier, reverseApproachFactory);
            result.Add(normalApprachFactory.Identifier, normalApprachFactory);
            result.Add(signalFact.Identifier, signalFact);
            result.Add(interlockingFact.Identifier, interlockingFact);
            result.Add(dimondFact.Identifier, dimondFact);
            result.Add(pointsFact.Identifier, pointsFact);
            result.Add(switchedFactory.Identifier, switchedFactory);
            result.Add(trackBoundaryFactory.Identifier, trackBoundaryFactory);
            result.Add(trackEndFactory.Identifier, trackEndFactory);
            result.Add(trackFactory.Identifier, trackFactory);
            result.Add(nodeFactory.Identifier, nodeFactory);
            //More section factories go here
            return result;
        }
    }
}
