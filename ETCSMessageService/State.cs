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

                    }
                    return true;
                }

            }
            else
            { 
                //Message length has yet to be set
                if (OutterBuffer.Count > 2)//if it has any data
                {
                    messageLength = (OutterBuffer[0] * 256 + OutterBuffer[1])+2;
                }
            }
            return false;
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
