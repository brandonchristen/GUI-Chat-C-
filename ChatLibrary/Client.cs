using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using LogLibrary;

namespace ChatLibrary
{
    
    public class Client
    {
        public MessageReceivedEventHandler MessageHandler;
        static string server = "127.0.0.1";
        static Int32 port = 13000;
        TcpClient TCPclient;
        NetworkStream stream;
        public bool flag = true;
        private  ILoggingService logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public Client(ILoggingService logger)
        {
            this.logger = logger;
        }




        /// <summary>
        /// connects to the server
        /// </summary>
        public void Connect()
        {
            try
            {
                TCPclient = new TcpClient(server, port);
                stream = TCPclient.GetStream();
                logger.Log("Client connected to server");
            }
            catch
            {

            }

        }
        /// <summary>
        /// close connection to server
        /// </summary>
        public  void Close()
        {
            try
            {
                // Close everything.
                TCPclient.Close();
                stream.Close();
                logger.Log("Client disconnected from server");
            }
            catch
            {

            }
        }


        /// <summary>
        /// listens for incoming messages from the server and sends them to the message recieved event handler
        /// </summary>
        public  void Listen()
        {
           while (flag)
            {
                string recieveMessage = "";
                try
                {
                    // Buffer to store the response bytes.
                    Byte[] data = new Byte[256];

                    // if there is data available, read it and send it back to the program.cs so it can print to console
                    if (stream.DataAvailable)
                    {
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        recieveMessage = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        logger.Log("Server: " + recieveMessage);
                        MessageHandler(this, new MessageArgs(recieveMessage));
                    }
                    else
                    {

                    }
                }
                catch
                {

                }

            }
        }

        /// <summary>
        /// sends SentMessage to the server
        /// </summary>
        /// <param name="SentMessage">
        /// The message the user types into txtMessage
        /// </param>
        public  void Talk(String SentMessage)
        {
            try
            {
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(SentMessage);
                // Send back a response.
                logger.Log("Client: " + SentMessage);
                stream.Write(msg, 0, msg.Length);
                
            }
            catch
            {

            }
        }
    }
}
