using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace SC2ReplaySync
{
    class WindowHandling
    {
        private IntPtr WindowHandle;
        private Thread WindowThread;
        public event StatusUpdateEventHandler StatusUpdate;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public WindowHandling()
        {
            WindowThread = new Thread(new ThreadStart(TryFindSC2Window));
            WindowThread.Start();
        }

        public bool WindowFound()
        {
            if (WindowHandle == IntPtr.Zero)
                return false;
            else
                return true;
        }

        public void StopWindowThread()
        {
            Log.LogMessage("Received request to stop window thread");
            if(WindowThread.IsAlive)
            {
                WindowThread.Abort();
                WindowThread.Join();
            }
        }

        ~WindowHandling()
        {
            StopWindowThread();
        }

        private void FindSC2Window()
        {
            var SC2Handle = FindWindow(null, "StarCraft II");

            if (SC2Handle != IntPtr.Zero && WindowHandle == IntPtr.Zero)
                Log.LogMessage("SC2 Window found, able to send key strokes");

            WindowHandle = SC2Handle;
            StatusUpdate();
        }

        public void TryFindSC2Window()
        {
            while (true)
            {
                FindSC2Window();
                Thread.Sleep(1000);
            }
        }

        public void SendReplayStart()
        {
            if (WindowHandle == IntPtr.Zero)
            {
                Log.LogMessage("Replay start was called but no window to send shit to, is SC2 open?");
            }

            SetForegroundWindow(WindowHandle);
            SendKeys.SendWait("p");
        }
    }
}
