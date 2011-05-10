using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;

namespace SC2ReplaySync
{
    class WindowHandling
    {
        private IntPtr WindowHandle;

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void FindSC2Window()
        {
            var SC2Handle = FindWindow(null, "StarCraft II");

            if (SC2Handle == IntPtr.Zero)
            {
                Log.LogMessage("SC2 Window not found yet.");
                return;
            }

            Log.LogMessage("SC2 Window found, able to send key strokes");
            WindowHandle = SC2Handle;
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
