using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace SC2ReplaySync
{
    partial class Network
    {
        public class Server : Network
        {
            
            private Dictionary<IPEndPoint, PingT> clients;

            ~Server()
            {
                StopThread();
            }

            private void UpdateClientPing(IPEndPoint endpoint, uint? ping)
            {
                var clientping = new PingT();
                if (clients.TryGetValue(endpoint, out clientping))
                {
                    if (ping != null)
                        clientping.Ping = (uint)ping;
                }
                else
                {
                    Log.LogMessage("New client " + endpoint );
                    clients.Add(endpoint, clientping);
                }
            }

            public void StartThread(int port)
            {
                thread = new Thread(new ParameterizedThreadStart(Run));
                thread.Name = "Server";
                thread.Start(port);
            }

            override protected void Start(object arg)
            {
                var port = (int)arg;
                var endpoint = new IPEndPoint(IPAddress.Any, port);

                try
                {
                    socket = new UdpClient(endpoint);
                    socket.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, true);
                    socket.DontFragment = true;
                }
                catch
                {
                    Log.LogMessage("Failed to create server on: " + endpoint);
                    return;
                }

                Log.LogMessage("Server created on: " + endpoint);

                ping = new PingT();
                clients = new Dictionary<IPEndPoint, PingT>();

                pingtimer = new System.Timers.Timer();
                pingtimer.Interval = 200;
                pingtimer.AutoReset = true;
                pingtimer.Elapsed += new System.Timers.ElapsedEventHandler(OnPingTimerExpired);
                pingtimer.Enabled = true;

                Program.GUI.StartReplay += new StartReplayEventHandler(SetupStartTimer);

                while (true)
                {
                    try
                    {
                        var clientendpoint = new IPEndPoint(IPAddress.Any, 0);
                        var data = socket.Receive(ref clientendpoint);

                        if (data.Length == 12)
                        {
                            int command = BitConverter.ToInt32(data, 0);
                            if (command == PING)
                            {
                                ReplyToPing(clientendpoint, data);
                                UpdateClientPing(clientendpoint, null); // add the new client
                                continue;
                            }

                            var receivedping = GetPingFromData(data);
                            ping.Ping = receivedping; // update global ping average
                            OnPingUpdate(ping.Ping);
                            UpdateClientPing(clientendpoint, receivedping); // update per-client average

                            if (command == START)
                            {
                                StartTimer((Program.StartAfterSeconds * 1000) - ((int)clients[clientendpoint].Ping / 2));
                            }
                        }
                        else
                            Log.LogMessage("got message of " + data.Length + " length");
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            protected override void SetupStartTimer(int startafter)
            {
                if (stopwatch == null)
                {
                    stopwatch = Stopwatch.StartNew();
                }
                
                var timestamp = stopwatch.ElapsedTicks;
                var buffer = new byte[4 + 8];
                Buffer.BlockCopy(BitConverter.GetBytes(START), 0, buffer, 0, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(timestamp), 0, buffer, 4, 8);

                foreach (var pair in clients)
                {
                    socket.Send(buffer, buffer.Length, pair.Key);
                }

                StartTimer(startafter * 1000);
            }

            public void OnPingTimerExpired(object source, System.Timers.ElapsedEventArgs e)
            {
                if (socket != null && !pingtimercancelled)
                {
                    foreach (var pair in clients)
                    {
                        SendTimestamp(pair.Key);
                    }
                }
            }
        }
    }
}
