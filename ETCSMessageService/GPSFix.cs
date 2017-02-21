using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public class GPSFix
    {
        public Byte FixType;

        public UInt16 LatDegrees;
        public UInt16 LatMinutes;
        public byte LatDecimal;
        public UInt32 LatMinutesFraction;
        public char LatHemishpere;
        public UInt16 LonDegrees;
        public UInt16 LonMinutes;
        public byte LonDecimal;
        public UInt32 LonMinutesFraction;
        public char LonHemisphere;

        public byte FIXTYPE;
        public byte FIXUSED;

        public uint SpeedKnotsInteger;
        public byte SpeedKnotsDecimal;
        public byte SpeedKnotsFraction;

        public UInt16 Hours;
        public UInt16 Minutes;
        public UInt16 Seconds;
        public byte Decimal_Point;
        public UInt16 Seconds_Fraction;

        public byte Offset;

    }
}
