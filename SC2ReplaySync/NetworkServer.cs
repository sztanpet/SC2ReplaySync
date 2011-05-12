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
        public class Server : Network
        {
            
            private Dictionary<IPEndPoint, PingT> clients;
            private PingT ping;

            ~Server()
            {
                StopThread();
            }

            private void UpdateClientPing(IPEndPoint endpoint, uint? ping)
            {
                var clientping = new PingT();
                if (clients.TryGetValue(endpoint, out clientping))
                {
                    Log.LogMessage("Client found in the dict");
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
                thread = new Thread(new ParameterizedThreadStart(Start));
                thread.Name = "Server";
                thread.Start(port);
            }

            private void Start(object arg)
            {
                var port = (int)arg;
                var endpoint = new IPEndPoint(IPAddress.Any, port);
                socket = new UdpClient(endpoint);
                socket.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, true);
                socket.DontFragment = true;
                Log.LogMessage("Server created on: " + endpoint);

                ping = new PingT();
                clients = new Dictionary<IPEndPoint, PingT>();

                pingtimer = new System.Timers.Timer();
                pingtimer.Interval = 200;
                pingtimer.AutoReset = true;
                pingtimer.Elapsed += new System.Timers.ElapsedEventHandler(OnPingTimerExpired);
                pingtimer.Enabled = true;

                var clientendpoint = new IPEndPoint(IPAddress.Any, 0);

                while (true)
                {
                    try
                    {
                        var data = socket.Receive(ref clientendpoint);
                        if (data.Length == 12)
                        {
                            int command = BitConverter.ToInt32(data, 0);
                            if (command == PING)
                            {
                                ReplyToPing(clientendpoint, data);
                                UpdateClientPing(clientendpoint, null); // ad // add the new client                        return;
                            }

                            var receivedping = GetPingFromData(data);
                            ping.Ping = receivedping; // update global ping average
                            UpdateClientPing(clientendpoint, receivedping); // update per-client average

                            if (command == START)
                            {
                                SetupStartTimer(Program.StartAfter - ((int)clients[clientendpoint].Ping / 2));
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            public void OnPingTimerExpired(object source, System.Timers.ElapsedEventArgs e)
            {
                if (socket != null && !pingtimercancelled)
                {
                    foreach (var pair in clients)
                    {
                        Log.LogMessage("sending ping to client: " + pair.Key);
                        SendTimestamp(pair.Key);
                    }
                }
            }
        }
    }
}
