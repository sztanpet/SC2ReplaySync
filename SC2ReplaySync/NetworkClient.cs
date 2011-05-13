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
                thread = new Thread(new ParameterizedThreadStart(Run));
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

            protected override void Start(object arg)
            {
                var destination = ((ThreadArguments)arg).destination;
                var port = ((ThreadArguments)arg).port;
                var endpoint = new IPEndPoint(IPAddress.Any, 0);

                socket = new UdpClient(destination, port);
                socket.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, true);
                Log.LogMessage("Connected to " + destination + " port: " + port);

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

                Program.GUI.StartReplay += new StartReplayEventHandler(SetupStartTimer);

                while (true)
                {
                    var data = new byte[0];

                    try
                    {
                        data = socket.Receive(ref endpoint);
                    }
                    catch
                    { 
                        continue;
                    }

                    if (data.Length == 12)
                    {
                        int command = BitConverter.ToInt32(data, 0);

                        if (command == PING)
                        {
                            ReplyToPing(null, data);
                            continue;
                        }

                        ping.Ping = GetPingFromData(data);
                        OnPingUpdate(ping.Ping);

                        if (command == START)
                        {
                            StartTimer((Program.StartAfterSeconds * 1000) - ((int)ping.Ping / 2));
                        }
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
