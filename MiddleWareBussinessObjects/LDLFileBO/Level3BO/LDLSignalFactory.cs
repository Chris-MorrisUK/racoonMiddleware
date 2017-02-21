using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiddleWareBussinessObjects.LDLFileBO
{
    public class LDLSignalFactory : LDLFactoryBase ,IParsableFactory
    {
        private const string SignalType = "TYPE";
        private const string ASPECTS = "ASPECTS";
        private const string SIGHTING_DISTANCE = "SIGHTING_DISTANCE";
        private const string STOP_POSITION = "STOP_POSITION";
        private const string NORMAL_APPROACH_TRIGGER = "NORMAL_APPROACH_TRIGGER";
        private const string REVERSE_APPROACH_TRIGGER = "REVERSE_APPROACH_TRIGGER";


        private string ident = "SIGNAL";
        public string Identifier
        {
            get { return ident; }
        }

        public LDLBOBase CreateItem(string[] definition, string id)
        {
            LDLSignal signal = new LDLSignal(id);
            foreach (string line in definition)
            {
                //if(!line.Contains(LDLSeperators.SECTION_START))
                string value = ParseItem(line).Trim();
                if (line.Contains(LDLInterlocking.Identifier))
                {
                    signal.InterlockingStr = value;
                    continue;
                }
                if (line.Contains(LDLRBC.Identity))
                {
                    signal.RadioBlockControLStr = value;
                    continue;
                }
                if (line.Contains(SignalType))
                {   
                    signal.SignalType = (LDLSignalTypeEnum)Enum.Parse(typeof(LDLSignalTypeEnum), value);
                    continue;
                }
                if (line.Contains(ASPECTS))
                {
                    signal.Aspects = (LDLSignalAspect)Enum.Parse(typeof(LDLSignalAspect), value);
                    continue;
                }
                if (line.Contains(SIGHTING_DISTANCE))
                {
                    signal.SightingDistance = decimal.Parse(value);
                    continue;
                }
                if (line.Contains(STOP_POSITION))
                {
                    signal.StopPosition = decimal.Parse(value);
                    continue;
                }
                if (line.Contains(NORMAL_APPROACH_TRIGGER))
                {
                    signal.NormalApproachStr = value;
                    continue;
                }
                if (line.Contains(REVERSE_APPROACH_TRIGGER))
                {
                    signal.ReverseApproachStr = value;
                    continue;
                }
                if (line.Contains(LDLDirectedLocation.DirectedLocationIdentifier))
                {
                    signal.Location = new LDLDirectedLocation(line);
                    continue;
                }
            }
            return signal;
        }

        
    }
}
