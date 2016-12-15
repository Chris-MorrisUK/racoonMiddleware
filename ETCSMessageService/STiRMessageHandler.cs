using DMCMessaging.MessageConstructs;
using DMCMessaging.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public class STiRMessageHandler
    {


        

        MessageConstructor m_messageManager;
        //ETCS and hence STiR refer to the server as the responder
     
        public static void Connect(IPEndPoint Responder)
        {           
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.BeginConnect(Responder, new AsyncCallback(AwaitMessage), client);       
        }

        private static void AwaitMessage(IAsyncResult ar)
        {

            // Retrieve the socket from the state object.
            Socket client = (Socket)ar.AsyncState;
            // Complete the connection.
            client.EndConnect(ar);
            //as soon as we connect we should be sent a 
            State state = new State(client);            
            // Begin receiving the data from the remote device.
            client.BeginReceive(state.Buffer, 0, State.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            
        }


        private static void ReceiveCallback(IAsyncResult ar)
        {

            // Retrieve the state object and the client socket 
            // from the asynchronous state object.
            State state = (State)ar.AsyncState;
            Socket client = state.WorkSocket;

            // Read data from the remote device.
            int bytesRead = client.EndReceive(ar);

            //state.BufferSwap();
            if (bytesRead > 0)
            {
                // Get the rest of the data.
                client.BeginReceive(state.Buffer, 0, State.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            else
                state.ProcessMessage();
        }
    }
}
