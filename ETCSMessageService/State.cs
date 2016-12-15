using DMCMessaging.MessageConstructs;
using DMCMessaging.Messages;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    /// <summary>
    ///  State object for receiving data from remote device.
    /// </summary>
    public class State
    {
        MessageConstructor m_messageManager;
        public State(Socket client)
        {
            WorkSocket = client;
            OutterBuffer = new List<byte>();

            m_messageManager = new MessageConstructor();
            m_messageManager.LoadDefinitions();
            messageLength = -1;
            TSAP = -1;
        }
        // Client socket.
        public Socket WorkSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] Buffer = new byte[BufferSize];
        int TSAP;

        public List<byte> OutterBuffer;
        private void bufferSwap()
        {
            OutterBuffer.AddRange(Buffer);
            Buffer = new byte[BufferSize];
        }

        int messageLength;
        public bool ProcessMessage()
        {
            bufferSwap();//ensure the full message is in output buffer

            if (messageLength > 0)
            {
                if (OutterBuffer.Count  == messageLength)
                {
                    PCBridgeMessageCommonHeader header = new PCBridgeMessageCommonHeader(OutterBuffer.ToArray());
                    if (header.pcBridgeMessageID == STIRConsts.MessageID_ListenRequest)
                        ProcessAsListenRequest();
                    else
                    {
                        ProcessFullMessage();
                    }
                    return true;
                }

            }
            else
            { 
                //Message length has yet to be set
                if (OutterBuffer.Count > 2)//if it has any data
                {
                    messageLength = (OutterBuffer[0] * 256 + OutterBuffer[1])+2;//Add 2 for the length of the size bytes which are not included
                }
            }
            return false;
        }

        private void ProcessFullMessage()
        {
            PCBridgeDataTransferMessage dataMessage = new PCBridgeDataTransferMessage(OutterBuffer.ToArray());
            dataMessage.messageManager = m_messageManager;
            dataMessage.Interpret();
            StirMessage NID = (StirMessage)dataMessage.nid_message;
            CompletePossitionReport posReport = null;
            switch (NID)
            {
                case StirMessage.NID_TrainPosUpdate://136
                    posReport=ProcessTrainPosMessage(dataMessage);
                    break;
                case StirMessage.NID_TrainStatus://136
                    posReport=ProcessTrainPosMessage(dataMessage);
                    break;
                case StirMessage.NID_NewSTiR_AOC://171
                    TriggerStir(dataMessage);
                    break;
                case StirMessage.NID_RevokeSTiR://173
                    RevokeStir(dataMessage);
                    break;
                default:
                    break;
            }
            if (posReport != null)
            {
                posReport.Source = NID;
            }
        }

        private void TriggerStir(PCBridgeDataTransferMessage dataMessage)
        { 

        }

        private void RevokeStir(PCBridgeDataTransferMessage dataMessage)
        {

        }

        private CompletePossitionReport ProcessTrainPosMessage(PCBridgeDataTransferMessage dataMessage)
        {           
                        
            CompletePossitionReport complete = new CompletePossitionReport();
            Packet gpsPacket = dataMessage.GetPacket(STIRConsts.Packet_OtherData);//packet 44
            GPSFix gpsDetails = ConstructGPSFix(gpsPacket);
            Packet posPacket = dataMessage.GetPacket(STIRConsts.Packet_PositionReport);//packet 0
            if(posPacket != null)
                complete.Basic = ConstructReport(posPacket);
            if(gpsDetails != null)             
                complete.GPSDetails = gpsDetails;
            Field Q_CABROLE = dataMessage.GetMessageField(STIRConsts.FieldName_Q_CABROLE);
            if(Q_CABROLE != null)
                complete.FrontCabNotBack = Q_CABROLE.value == 0;

            Field Q_STIRSTATUS = dataMessage.GetMessageField(STIRConsts.FieldName_Q_CABROLE);
            if (Q_STIRSTATUS != null)
                complete.Stir_Status = (StirStatusEnum)(byte)Q_STIRSTATUS.value;

            complete.NID_Engine = dataMessage.GetMessageField(STIRConsts.FieldName_NID_ENGINE).value;
            complete.TSAP = dataMessage.TSAP;
            return complete;
        }

        private PositionReport ConstructReport(Packet posPacket)
        {
            PositionReport report = new PositionReport();
            report.Scale = (byte)posPacket.GetField(STIRConsts.FieldName_SCALE).value;
            UInt32 NID_LRBG = posPacket.GetField(STIRConsts.FieldName_NID_LRBG).value;
            report.NID_LRBG = NID_LRBG;//we're not breaking this down into sub fields
            report.BaliseGroupID = (UInt16)(NID_LRBG & 0x3FFF);
            report.BaliseCountryID = (UInt16)(NID_LRBG >> 14);

            report.DistanceFromBallise = (UInt16)posPacket.GetField(STIRConsts.FieldName_D_LRGB).value;
            report.DirectionFromBallise = (byte)posPacket.GetField(STIRConsts.FieldName_Q_DIRLRBG).value;
            report.DirectionOfTrain = (byte)posPacket.GetField(STIRConsts.FieldName_Q_DIRTRAIN).value;
            report.DoubtOver = (UInt16)posPacket.GetField(STIRConsts.FieldName_L_DOUBTOVER).value;
            report.DoubtUnder = (UInt16)posPacket.GetField(STIRConsts.FieldName_L_DOUBTUNDER).value;
            report.TrainIntegrity = (byte)posPacket.GetField(STIRConsts.FieldName_Q_LENGTH).value;
            report.SafeTrainLength = (UInt16)posPacket.GetField(STIRConsts.FieldName_L_TRAININT).value;
            report.TrainSpeed = (byte)posPacket.GetField(STIRConsts.FieldName_V_TRAIN).value;
            //report.TrainDirectionRelativeToBallise = (byte)posPacket.GetField(STIRConsts.).value;
            report.Mode = (byte)posPacket.GetField(STIRConsts.FieldName_M_MODE).value;
            report.Level = (byte)posPacket.GetField(STIRConsts.FieldName_M_LEVEL).value;

            return report;
        }

        private static GPSFix ConstructGPSFix(Packet gpsPacket)
        {
            if (gpsPacket.GetField(STIRConsts.FieldName_User).value != STIRConsts.NID_XUser_STIR)
                return null;//Packet not intended for us
            GPSFix theFix = new GPSFix();

            //Co-ordinates are D M'mm.mmm'
            theFix.LatDegrees = (UInt16)gpsPacket.GetField(STIRConsts.FieldName_LatDegrees).value;
            theFix.LatMinutes = (UInt16)gpsPacket.GetField(STIRConsts.FieldName_LatMinutes).value;
            theFix.LatDecimal = (byte)gpsPacket.GetField(STIRConsts.FieldName_LatDecimal).value;
            theFix.LatMinutesFraction = gpsPacket.GetField(STIRConsts.FieldName_LatFraction).value;
            theFix.LatHemishpere = (char)gpsPacket.GetField(STIRConsts.FieldName_LatHemisphere).value;
            theFix.LonDegrees = (UInt16)gpsPacket.GetField(STIRConsts.FieldName_LonDegrees).value;
            theFix.LonMinutes = (UInt16)gpsPacket.GetField(STIRConsts.FieldName_LonMinutes).value;
            theFix.LonDecimal = (byte)gpsPacket.GetField(STIRConsts.FieldName_LonDecimal).value;
            theFix.LonMinutesFraction = gpsPacket.GetField(STIRConsts.FieldName_LonFraction).value;
            //Speed in knots
            theFix.SpeedKnotsInteger = gpsPacket.GetField(STIRConsts.FieldName_SPEED_KNOTS_INTEGER).value;
            theFix.SpeedKnotsDecimal = (byte)gpsPacket.GetField(STIRConsts.FieldName_SPEED_DECIMAL_POINT).value;
            theFix.SpeedKnotsFraction = (byte)gpsPacket.GetField(STIRConsts.FieldName_SPEED_KNOTS_FRACTION).value;
            //Fix time
            theFix.Hours = (ushort)gpsPacket.GetField(STIRConsts.FieldName_Hours).value;
            theFix.Minutes = (ushort)gpsPacket.GetField(STIRConsts.FieldName_Minutes).value;
            theFix.Seconds = (ushort)gpsPacket.GetField(STIRConsts.FieldName_Seconds).value;
            theFix.Decimal_Point = (byte)gpsPacket.GetField(STIRConsts.FieldName_Hours).value;
            theFix.Seconds_Fraction = (ushort)gpsPacket.GetField(STIRConsts.FieldName_Seconds_Fraction).value;
            //other
            theFix.FixType = (byte)gpsPacket.GetField(STIRConsts.FieldName_FIXTYPE).value;
            theFix.Offset = (byte)gpsPacket.GetField(STIRConsts.FieldName_Offset).value;

            return theFix;
        }

        private void ProcessAsListenRequest()
        {
            PCBridgeDataTransferMessage t_pcBridgeData = new PCBridgeDataTransferMessage(OutterBuffer.ToArray());
            t_pcBridgeData.messageManager = m_messageManager;
            t_pcBridgeData.Interpret();
            TSAP =  t_pcBridgeData.TSAP;
        }


        //public void ProcessMessage()
        //{
        //    // Create a PCBridge message populating it with the newly received buffer.
        //    PCBridgeDataTransferMessage t_pcBridgeData = new PCBridgeDataTransferMessage(OutterBuffer.ToArray());

        //    // Assign the message manager
        //    t_pcBridgeData.messageManager = m_messageManager;

        //    // Interpret the message then access its value…
        //    t_pcBridgeData.Interpret();
            
        //    uint t_etcsMessageLength = t_pcBridgeData.GetMessageField("L_MESSAGE").value;
            
        //    if (t_pcBridgeData.pcBridgeMessageID == STIRConsts.MessageID_ListenRequest)//6 (dec)
        //    {
        //        uint TSAP = t_pcBridgeData.TSAP;
        //    }
           
        //}
    }
}
