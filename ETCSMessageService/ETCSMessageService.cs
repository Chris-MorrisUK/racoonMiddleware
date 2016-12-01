using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETCSMessageService
{
    public partial class ETCSMessageService : ServiceBase
    {


        // ManualResetEvent instances signal completion.
        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        // The response from the remote device.
        private static String response = String.Empty;



        public ETCSMessageService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

        }




    // Thread signal.
    public static ManualResetEvent acceptDone = new ManualResetEvent(false);
    private static volatile bool ServerRunning;



    public static void StartListening() {
        // Data buffer for incoming data.
        byte[] bytes = new Byte[1024];

        IPAddress ipAddress = IPAddress.Parse(Properties.Settings.Default.LocalIPAddress);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Properties.Settings.Default.Port);


        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );

        // Bind the socket to the local endpoint and listen for incoming connections.
        try {
            listener.Bind(localEndPoint);
            listener.Listen(100);
            ServerRunning = true;
            while (ServerRunning)
            {
                // Set the event to nonsignaled state.
                acceptDone.Reset();

                // Start an asynchronous socket to listen for connections.
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept( new AsyncCallback(AcceptCallback), listener );

                // Wait until a connection is made before continuing.
                acceptDone.WaitOne();
            }

        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }


    }

    public static void AcceptCallback(IAsyncResult ar) 
    {
        // Signal the main thread to continue.
        acceptDone.Set();

        // Get the socket that handles the client request.
        Socket listener = (Socket) ar.AsyncState;
        Socket handler = listener.EndAccept(ar);

        // Create the state object.
        State state = new State(handler);
        handler.BeginReceive( state.Buffer, 0, State.BufferSize, 0, new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        // Retrieve the state object and the handler socket
        // from the asynchronous state object.
        State state = (State)ar.AsyncState;
        Socket handler = state.WorkSocket;

        // Read data from the client socket. 
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {


        
        
            // Not all data received. Get more.
            handler.BeginReceive(state.Buffer, 0, State.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        
        }
    }
    

    private static void Send(Socket handler, String data) {
        // Convert the string data to byte data using ASCII encoding.
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar) {
        try {
            // Retrieve the socket from the state object.
            Socket handler = (Socket) ar.AsyncState;

            // Complete sending the data to the remote device.
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        } catch (Exception e) {
            Console.WriteLine(e.ToString());
        }
    }

        protected override void OnStop()
        {
            ServerRunning = false;
        }
    }
}
;