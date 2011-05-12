using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SC2ReplaySync
{
    partial class Network
    {
        public class Client : Network
        {

            public void StartThread(string destination, int port)
            {
                thread = new Thread(new ParameterizedThreadStart(Start));
                thread.Name = "Client";

                var arguments = new ThreadArguments();
                arguments.destination = destination;
                arguments.port = port;

                thread.Start(arguments);
            }

            class ThreadArguments
            {
                public string destination;
                public int port;
            }

            private void Start(object arg)
            {
                var destination = ((ThreadArguments)arg).destination;
                var port = ((ThreadArguments)arg).port;
                Log.LogMessage("connecting to " + destination + " port: " + port);
                socket = new UdpClient(destination, port); // exception handling
                socket.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, true);
                try
                {
                    socket.DontFragment = true;
                }
                catch { }

                ping = new PingT();

                pingtimer = new System.Timers.Timer();
                pingtimer.Interval = 200;
                pingtimer.AutoReset = true;
                pingtimer.Elapsed += new System.Timers.ElapsedEventHandler(OnPingTimerExpired);
                pingtimer.Enabled = true;

                while (true)
                {
                    var endpoint = new IPEndPoint(IPAddress.Any, 0);
                    var data = socket.Receive(ref endpoint);

                    if (data.Length == 12)
                    {
                        int command = BitConverter.ToInt32(data, 0);

                        if (command == PING)
                        {
                            ReplyToPing(endpoint, data);
                            return;
                        }
                        else if (command == START)
                        {
                            SetupStartTimer(Program.StartAfter - ((int)ping.Ping / 2));
                        }

                        ping.Ping = GetPingFromData(data);
                        OnPingUpdate();
                    }
                }
            }

            public void OnPingTimerExpired(object source, System.Timers.ElapsedEventArgs e)
            {
                if (!pingtimercancelled)
                    SendTimestamp(null);
            }
        }
    }
}
