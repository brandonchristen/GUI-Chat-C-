using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary;
using System.Windows.Forms;
using System.Threading;

namespace Assignment2
{
    public partial class Form1 : Form
    {
        Thread workerThread;
        Client client = new Client();

        public Form1()
        {
            client.MessageHandler += new ChatLibrary.MessageReceivedEventHandler(newMessage);
            InitializeComponent();
        }

        private void exit_click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connect_click(object sender, EventArgs e)
        {
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
                txtConvo.Text += message;
            }));
            }

            else
            {
                txtConvo.Text += message;
            }
        }

    }
}
