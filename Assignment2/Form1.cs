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

        public Form1()
        {
            FileName = "C:\\" + DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss")+".log";
            client.MessageHandler += new ChatLibrary.MessageReceivedEventHandler(newMessage);
            InitializeComponent();
        }

        private void exit_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connect_click(object sender, EventArgs e)
        {
            btnSend.Enabled = true;
            client.Connect();
            Thread workerThread = new Thread(client.Listen);
            workerThread.Name = "Listening Thread";
            workerThread.Start();
        }

        private void disconnect_click(object sender, EventArgs e)
        {
            client.Close();
        }

        private void newMessage(string message)
        {
            if (txtConvo.InvokeRequired)
            {
                txtConvo.Invoke(new MethodInvoker(delegate ()
            {

                txtConvo.Text += "\n" + "Server: " + message;
                line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + "Server: " + message + "\n";
                logger.WriteMessage(FileName, line);
            }));
            }

            else
            {
                txtConvo.Text += "\n" + "Server: " + message;
                line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + "Server: " + message + "\n";
                logger.WriteMessage(FileName, line);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string UserMessage = txtMessage.Text;
            txtConvo.Text += "\n" + "Client: " + UserMessage;
            line = DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") + "Client: " + UserMessage + "\n";
            logger.WriteMessage(FileName, line);
            client.Talk(UserMessage);
        }
    }
}
