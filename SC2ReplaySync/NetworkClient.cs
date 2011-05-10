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
        private IPEndPoint serverendpoint;
        private Int64 lasttimestamp;
        private PingT ping;
        private Thread pingthread;

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
                    Thread.Sleep((int)ping.Ping / 2);
                    Program.SCWindow.SendReplayStart();
                }

                ping.Ping = GetPingFromData(data);
                var eventarg = new PingEventArgs();
                eventarg.ping = ping;
                OnPingUpdate(eventarg);
            }
            else
            {
                Log.LogMessage("Received message with length: " + data.Length + " disregarding");
            }
        }

        private void Connect(IPEndPoint endpoint)
        {
            server = new UdpClient(endpoint); // exception handling
            server.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, true);
            server.DontFragment = true;
            server.BeginReceive(new AsyncCallback(OnClientReceive), null);
            ping = new PingT();
            pingthread = new Thread(new ThreadStart(SendPingRequest));
        }

        private void Disconnect()
        {

            if (pingthread.IsAlive)
            {
                pingthread.Abort();
                pingthread.Join();
            }

            if (server.Client.Connected)
                server.Close();
        }

        private void SendPingRequest()
        {
            while (true)
            {
                SendTimestamp(server, serverendpoint);
                Thread.Sleep(200);
            }
        }
    }
}
