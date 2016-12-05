using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public static class STIRConsts
    {
        public const byte MessageID_ListenRequest = 6;
        public const byte NID_TrainPosUpdate = 136;
        public const byte Packet_OtherData = 44;
        public const byte Packet_PositionReport = 0;

        #region Field Names
        public const string FieldName_LatDegrees = "M_LAT_DEGREES";
        public const string FieldName_LatMinutes = "M_LAT_MINUTES_INTEGER";
        public const string FieldName_LatDecimal = "M_LAT_DECIMAL_POINT";
        public const string FieldName_LatFraction = "M_LAT_MINUTES_FRACTION";
        public const string FieldName_LatHemisphere = "M_LAT_HEMISPHERE";

        public const string FieldName_LonDegrees = "M_LON_DEGREES";
        public const string FieldName_LonMinutes = "M_LON_MINUTES_INTEGER";
        public const string FieldName_LonDecimal = "M_LON_DECIMAL_POINT";
        public const string FieldName_LonFraction = "M_LON_MINUTES_FRACTION";
        public const string FieldName_LonHemisphere = "M_LON_HEMISPHERE";


        #endregion
    }
}
