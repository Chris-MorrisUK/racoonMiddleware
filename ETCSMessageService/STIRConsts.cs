using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public static class STIRConsts
    {
        public const byte MessageID_ListenRequest = 6;
       /* public const byte NID_TrainPosUpdate = 136;
        public const byte NID_NewSTiR_AOC  = 171;
        public const byte NID_TrainStatus = 172;
        public const byte NID_RevokeSTiR = 173;*/

        #region packetIDs
        public const byte Packet_OtherData = 44;
        public const byte Packet_PositionReport = 0;
        #endregion
        public const UInt16 NID_XUser_STIR = 2;

        #region Field Names
        #region GPS Packet
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

        public const string FieldName_User = "NID_XUSER";
        public const string FieldName_44_TYPE = "NID_44_TYPE";
        public const string FieldName_FIXTYPE = "M_FIXTYPE";
        public const string FieldName_FIXUSED = "M_FIXUSED";

        public const string FieldName_SPEED_KNOTS_INTEGER = "M_SPEED_KNOTS_INTEGER";
        public const string FieldName_SPEED_DECIMAL_POINT = "M_SPEED_KNOTS_DECIMAL_POINT";
        public const string FieldName_SPEED_KNOTS_FRACTION = "M_SPEED_KNOTS_FRACTION";

        public const string FieldName_Hours= "M_UTDATE_HOURS";
        public const string FieldName_Minutes = "M_UTDATE_MINUTES";
        public const string FieldName_Seconds = "M_UTDATE_SECONDS";
        public const string FieldName_Decimal_Point = "M_UTDATE_DECIMPAL_POINT";
        public const string FieldName_Seconds_Fraction = "M_UTDATE_SECONDS_FRACTION";

        public const string FieldName_Offset = "M_LEN";
        #endregion
        #region Pos Packet
        public const string FieldName_PACKETID = "NID_PACKET";
        public const string FieldName_PACKET_Length = "L_PACKET";
        public const string FieldName_SCALE = "Q_SCALE";
        /// <summary>
        /// Ballise Group ID
        /// </summary>
        public const string FieldName_NID_LRBG = "NID_LRBG";
        public const string FieldName_D_LRGB = "D_LRGB";
        public const string FieldName_Q_DIRLRBG = "Q_DIRLRBG";
        public const string FieldName_Q_DLRBG = "Q_DLRBG";
        public const string FieldName_L_DOUBTOVER = "L_DOUBTOVER";
        public const string FieldName_L_DOUBTUNDER = "L_DOUBTUNDER";
        public const string FieldName_Q_LENGTH = "Q_LENGTH";
        public const string FieldName_L_TRAININT = "L_TRAININT";
        public const string FieldName_V_TRAIN = "V_TRAIN";
        public const string FieldName_Q_DIRTRAIN = "Q_DIRTRAIN";
        public const string FieldName_M_MODE = "M_MODE";
        public const string FieldName_M_LEVEL = "M_LEVEL";
        public const string FieldName_NID_STM = "NID_STM";

        #endregion
        #region Multiple messages and fields, including Message 136
        public const string FieldName_Q_CABROLE = "Q_CABROLE";
        public const string FieldName_Q_STIRSTATUS = "Q_STIRSTATUS";
        public const string FieldName_NID_ENGINE = "NID_ENGINE";
        #endregion
        #endregion
    }
}
