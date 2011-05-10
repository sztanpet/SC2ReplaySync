using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SC2ReplaySync
{
    partial class Network
    {
        private UdpClient serversocket;
        private UdpClient[] clients;

        private void OnServerReceive(IAsyncResult result)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, 0);
            var data = serversocket.EndReceive(result, ref endpoint);
            if (data.Length == 12)
            {
                int command = BitConverter.ToInt32(data, 0);
                if (command == PING)
                {
                    ReplyToPing(serversocket, endpoint, data);
                    return;
                }

                Int64 timestamp = BitConverter.ToInt64(data, 4);
                // per-client pings?
            }
        }

        private void Create(int port = 9996)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, port);
            serversocket = new UdpClient(endpoint);
            serversocket.Client.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, true);
            serversocket.DontFragment = true;
            serversocket.BeginReceive(new AsyncCallback(OnServerReceive), null);
        }
    }
}
