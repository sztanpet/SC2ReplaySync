using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SC2ReplaySync
{
    
    static class Program
    {
        public const int StartAfterSeconds = 5;
        public static WindowHandling SCWindow;
        public static MainGUI GUI;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GUI = new MainGUI();
            Application.Run(GUI);
        }
    }
}
