using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace SC2ReplaySync
{
    
    static class Program
    {
        public static WindowHandling SCWindow;
        public delegate void PingUpdateEventHandler(object sender, EventArgs e);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainGUI());
        }
    }
}
