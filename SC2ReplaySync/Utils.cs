using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SC2ReplaySync
{
    class Utils
    {
    }

    class Log
    {
        public static event LogboxUpdateEventHandler LogboxUpdate;
        internal static void LogMessage(string message)
        {
            // MainGui.LogBox-hoz extra sor az uzenettel
            //LogBox.text += "\n" + message;
            //throw new NotImplementedException();
            System.Diagnostics.Trace.WriteLine(message);
            if (LogboxUpdate != null)
            {
                var e = new MainGUI.LogboxEventArgs();
                e.message = message;
                LogboxUpdate(null, e);
            }
        }
    }
}
