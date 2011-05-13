using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;

namespace SC2ReplaySync
{
    class WindowHandling
    {
        private IntPtr windowhandle;
        private System.Timers.Timer timer;
        private bool timerstopped = false;
        public event StatusUpdateEventHandler StatusUpdate;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public WindowHandling()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(FindSC2Window);
            timer.Enabled = true;
        }

        public bool WindowFound()
        {
            if (windowhandle == IntPtr.Zero)
                return false;
            else
                return true;
        }

        public void StopWindowFinding()
        {
            timerstopped = true;
            timer.Elapsed -= new System.Timers.ElapsedEventHandler(FindSC2Window);
            timer.Enabled = false;
        }

        ~WindowHandling()
        {
            timerstopped = true;
            timer.Enabled = false;
        }

        private void FindSC2Window(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (timerstopped)
                return;

            var SC2Handle = FindWindow(null, "StarCraft II");

            if (SC2Handle != IntPtr.Zero && windowhandle == IntPtr.Zero)
                Log.LogMessage("SC2 Window found, able to send key strokes");

            windowhandle = SC2Handle;
            StatusUpdate();
        }

        public void SendReplayStart()
        {
            if (windowhandle == IntPtr.Zero)
            {
                Log.LogMessage("Replay start was called but no window to send shit to, is SC2 open?");
            }

            SetForegroundWindow(windowhandle);
            SendKeys.SendWait("p");
        }
    }
}
