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
        private UdpClient server;
        
        private void OnClientReceive(IAsyncResult result)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, 0);
            var data = server.EndReceive(result, ref endpoint);

            if (data.Length == 12)
            {
                int command = BitConverter.ToInt32(data, 0);

                if (command == PING)
                {
                    ReplyToPing(server, endpoint, data);
                    return;
                }
                else if (command == START)
                {
                    SetupStartTimer(Program.StartAfter - ((int)ping.Ping / 2));
                }

                ping.Ping = GetPingFromData(data);
                OnPingUpdate();
            }
            else
            {
                Log.LogMessage("Received message with length: " + data.Length + " disregarding");
            }
        }

        public void Connect(string destination, int port)
        {
            Log.LogMessage("connecting to " + destination + " port: " + port);
            server = new UdpClient(destination, port); // exception handling
            server.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, true);
            server.DontFragment = true;
            server.BeginReceive(new AsyncCallback(OnClientReceive), null);

            ping = new PingT();

            var timer = new System.Timers.Timer();
            timer.Interval = 200;
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(OnPingTimerExpired);
            timer.Enabled = true;
        }

        private void Disconnect()
        {
            if (server.Client.Connected)
                server.Close();
        }

        public void OnPingTimerExpired(object source, System.Timers.ElapsedEventArgs e)
        {
            if(server.Client.Connected)
                SendTimestamp(server, null);
        }
    }
}
