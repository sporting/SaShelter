using ShelterHttpServer;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;

namespace ShelterServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            IP = GetLocalIPAddress();

            Text = string.Format("{0} {1}", Text, IP);

            txbWebFolder.Text = Directory.GetCurrentDirectory();

            Random r = new Random();
            int newPort = r.Next(49152, 65535);

            txbWebPort.Text = newPort.ToString();
        }

        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        private HTTPServer myServer = null;
        private string IP;
        private int PORT;
        private string WebUrl { get { return string.Format("http://{0}:{1}", IP, PORT); } }


        private bool SwitchOnOff { get; set; }
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!SwitchOnOff)
            {
                string myFolder = txbWebFolder.Text.Trim();
                PORT = Convert.ToInt32(txbWebPort.Text.Trim());

                myServer = new HTTPServer(myFolder, IP, PORT);
                myServer.Start();
                Log("Server is running: " + Environment.NewLine + WebUrl);
                btnRun.Text = "Stop";
            }
            else
            {
                if (myServer != null)
                {
                    myServer.Stop();
                    Log("Server is stop" );
                }
                btnRun.Text = "Run";
            }

            SwitchOnOff = !SwitchOnOff;
        }

        private void Log(string message)
        {
            txbLog.AppendText(message + Environment.NewLine + Environment.NewLine);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (myServer != null)
            {
                myServer.Stop();
                Log("Server is stop");
            }
        }
    }
}
