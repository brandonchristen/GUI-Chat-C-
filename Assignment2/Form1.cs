using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary;
using LogLibrary;
using System.Windows.Forms;
using System.Threading;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        Thread workerThread;
        Client client = new Client();
        Logger logger = new Logger();
        string FileName;
        string line;

        /// <summary>
        /// Starts the application and also writes to the log
        /// </summary>
        public Form1()
        {
            FileName = "E:\\" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".log";
            client.MessageHandler += new ChatLibrary.MessageReceivedEventHandler(newMessage);
            InitializeComponent();
        }

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
            line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Client Connected From Server " + "\n";
            logger.WriteMessage(FileName, line);
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
            line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Client Disconnected From Server " + "\n";
            logger.WriteMessage(FileName, line);
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
                line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Server: " + MessageArguments.message + "\n";
                logger.WriteMessage(FileName, line);
            }));
            }

            else
            {
                txtConvo.Text += "Server: " + MessageArguments .message +  "\r\n";
                line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Server: " + MessageArguments.message + "\n";
                logger.WriteMessage(FileName, line);
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
            line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + " Client: " + UserMessage + "\n";
            logger.WriteMessage(FileName, line);
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