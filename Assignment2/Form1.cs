using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary;
//using LogLibrary;
using Logging;
using System.Windows.Forms;
using System.Threading;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        Thread workerThread;
        private Client client;
        string line;

        public Form1( Client client)
        {
            this.client = client;
            client.MessageHandler += new ChatLibrary.MessageReceivedEventHandler(newMessage);
            InitializeComponent();
        }
        /// <summary>
        /// Starts the application and also writes to the log
        /// </summary>

        /// <summary>
        /// Exit the application safely
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_click(object sender, EventArgs e)
        {
            client.flag = true;
            client.Close();
            Application.Exit();
        }

        /// <summary>
        /// Connects to the server, 
        /// writes a line into the log file
        /// and starts a thread listining for incoming messages from server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connect_click(object sender, EventArgs e)
        {
            btnSend.Enabled = true;
            client.Connect();
            Thread workerThread = new Thread(client.Listen);
            workerThread.Name = "Listening Thread";
            workerThread.Start();
        }

        /// <summary>
        /// Disconnect from the server, and write a line in the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnect_click(object sender, EventArgs e)
        {
            client.Close();
        }

        /// <summary>
        /// When a message is recieved display it in txtConvo
        /// prepare a line to be written into the text file
        /// write a line to the log file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="MessageArguments"></param>
        private void newMessage(object sender, MessageArgs MessageArguments)
        {
            if (txtConvo.InvokeRequired)
            {
                txtConvo.Invoke(new MethodInvoker(delegate ()
            {

                txtConvo.Text += "Server: " + MessageArguments.message + "\r\n";

            }));
            }

            else
            {
                txtConvo.Text += "Server: " + MessageArguments .message +  "\r\n";
                
            }
        }


        /// <summary>
        /// Shows client message in txtConvo
        /// prepare a line to be written into the text file
        /// write a line to the log file
        /// sends user message to server
        /// clears txt message so user dosen't have to delete previous message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string UserMessage = txtMessage.Text;
            txtConvo.Text += "Client: " + UserMessage + "\r\n";
            client.Talk(UserMessage);
            txtMessage.Clear();
        }

        /// <summary>
        /// Handle the closing of the application gracefully
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.flag = true;
            client.Close();
            Application.Exit();
        }
    }
}