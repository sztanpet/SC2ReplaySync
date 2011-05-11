using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SC2ReplaySync
{
    public delegate void LogboxUpdateEventHandler(object sender, MainGUI.LogboxEventArgs e);
    public delegate void StatusUpdateEventHandler();
    public partial class MainGUI : Form
    {
        delegate void SetLogboxMessage(object sender, LogboxEventArgs e);
        public MainGUI()
        {
            InitializeComponent();
        }

        private void MainGUI_Load(object sender, EventArgs e)
        {
            ToolStripStatusLabel.Text = "";
            LogTextBox.Text += "Newest version at: http://github.com/sztanpet/SC2ReplaySync";

            Log.LogboxUpdate += new LogboxUpdateEventHandler(Logbox_Update);
            Program.SCWindow = new WindowHandling();
            Program.SCWindow.StatusUpdate += new StatusUpdateEventHandler(StatusLabel_Update);

            Program.Netw = new Network();
            Program.Netw.PingUpdate += new StatusUpdateEventHandler(StatusLabel_Update);
            try
            {
                Program.Netw.Connect("192.168.1.3", 9996);
            }
            catch
            {
                Log.LogMessage("Connect failed");
            }
        }
        
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            // TODO hook up connect
        }

        private void CreateServerButton_Click(object sender, EventArgs e)
        {
            // TODO hook up create
        }

        private void StartReplayButton_Click(object sender, EventArgs e)
        {
            // TODO hook up sendreplay
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            Program.SCWindow.StopWindowThread();
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

        private void StatusLabel_Update()
        {
            // TODO megnezni hogy van e
            var statustext = "";
            var ping = Program.Netw.GetPing();
            if (ping > 0)
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
    }
}
