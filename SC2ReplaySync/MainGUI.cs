using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SC2ReplaySync
{
    public delegate void LogboxUpdateEventHandler(object sender, MainGUI.LogboxEventArgs e);
    public delegate void StatusUpdateEventHandler();
    public delegate void PingUpdateEventHandler(uint ping);
    public delegate void StartReplayEventHandler(int startafter);

    public partial class MainGUI : Form
    {
        delegate void SetLogboxMessage(object sender, LogboxEventArgs e);
        public event StartReplayEventHandler StartReplay;
        private string PortCharSet = "0123456789\b";
        private string IpCharSet = "0123456789.\b";
        private Network.Client NetworkClient;
        private Network.Server NetworkServer;
        private uint ping;

        public MainGUI()
        {
            InitializeComponent();
        }

        private void MainGUI_Load(object sender, EventArgs e)
        {
            ToolStripStatusLabel.Text = "";
            LogTextBox.Text += "Homepage: http://github.com/sztanpet/SC2ReplaySync" + Environment.NewLine;

            Log.LogboxUpdate += new LogboxUpdateEventHandler(Logbox_Update);
            Program.SCWindow = new WindowHandling();
            Program.SCWindow.StatusUpdate += new StatusUpdateEventHandler(StatusLabel_Update);
            
            NetworkClient = new Network.Client();
            NetworkClient.PingUpdate += new PingUpdateEventHandler(Ping_Update);
            NetworkServer = new Network.Server();
            NetworkServer.PingUpdate += new PingUpdateEventHandler(Ping_Update);
        }
        
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (NetworkClient.ThreadIsAlive())
            {
                NetworkClient.StopThread();
                ConnectButton.Text = "Connect";
                CreateServerButton.Enabled = true;
                StartReplayButton.Enabled = false;
                return;
            }

            int port = Convert.ToInt32(PortTextBox.Text);
            if (port > 65535)
            {
                Log.LogMessage("Invalid port specified");
                return;
            }

            try
            {
                System.Net.IPAddress.Parse(IPTextBox.Text);
            }
            catch
            {
                Log.LogMessage("Invalid IP address");
                return;
            }

            try
            {
                NetworkClient.StartThread(IPTextBox.Text, port);
            }
            catch
            {
                Log.LogMessage("Failed connecting");
            }

            if (NetworkClient.ThreadIsAlive())
            {
                ConnectButton.Text = "Disconnect";
                CreateServerButton.Enabled = false;
                StartReplayButton.Enabled = true;
            }
        }

        private void CreateServerButton_Click(object sender, EventArgs e)
        {
            if (NetworkServer.ThreadIsAlive())
            {
                NetworkServer.StopThread();
                CreateServerButton.Text = "Create server";
                ConnectButton.Enabled = true;
                StartReplayButton.Enabled = false;
                return;
            }

            int port = Convert.ToInt32(PortTextBox.Text);
            if (port > 65535)
            {
                Log.LogMessage("Invalid port specified");
                return;
            }

            try
            {
                NetworkServer.StartThread(port);
            }
            catch
            {
                Log.LogMessage("Unable to create server");
            }

            if (NetworkServer.ThreadIsAlive())
            {
                CreateServerButton.Text = "Stop server";
                ConnectButton.Enabled = false;
                StartReplayButton.Enabled = true;
            }
        }

        private void StartReplayButton_Click(object sender, EventArgs e)
        {
            
            if (StartReplay == null)
                return;

            StartReplay(Program.StartAfterSeconds);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Program.SCWindow.StopWindowFinding();
            NetworkServer.StopThread();
            NetworkClient.StopThread();
        }

        private void Logbox_Update(object sender, LogboxEventArgs e)
        {
            if (LogTextBox.InvokeRequired)
            {
                var deleg = new SetLogboxMessage(Logbox_Update);
                try // to catch ObjectDisposedException if there are still events outstanding when the MainGUI is already gone
                {
                    Invoke(deleg, sender, e);
                }
                catch
                {
                }
            }
            else
            {
                LogTextBox.Text += e.message + Environment.NewLine;
                LogTextBox.SelectionStart = LogTextBox.Text.Length;
                LogTextBox.ScrollToCaret();
                LogTextBox.Refresh();
            }
        }

        private void Ping_Update(uint clientping)
        {
            ping = clientping;
            StatusLabel_Update();
        }

        private void StatusLabel_Update()
        {
            var statustext = "";
            
            if (ping != 0)
            {
                statustext += "Avg Ping: " + ping + " | ";
            }

            if (Program.SCWindow.WindowFound())
            {
                statustext += "SC2 Found";
            }
            else
            {
                statustext += "SC2 Not running";
            }
            
            ToolStripStatusLabel.Text = statustext;
        }

        public class LogboxEventArgs : EventArgs
        {
            public string message;
        }

        private void PortTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!PortCharSet.Contains(e.KeyChar.ToString()) && (!char.IsControl(e.KeyChar) && e.KeyChar.ToString() != "v"))
                e.Handled = true;
        }

        private void IPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!IpCharSet.Contains(e.KeyChar.ToString()) && (!char.IsControl(e.KeyChar) && e.KeyChar.ToString() != "v"))
                e.Handled = true;
        }
    }
}
