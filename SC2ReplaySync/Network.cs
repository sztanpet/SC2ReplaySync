using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;

namespace SC2ReplaySync
{
    
    partial class Network
    {
        public struct PingT
        {
            // a maximum elteres amit toleralunk
            private const double maxsd = 80;
            private double sd;
            private uint count;
            private UInt64 sum;
            private UInt64 sumsquared;
            private uint min;
            private uint max;
            private double mean;
            
            public uint Ping
            {
                get
                {
                    return (uint)mean;
                }

                set
                {

                    if (count == 0 || value < min)
                        min = value;
                    if (count == 0 || value > max)
                        max = value;

                    count++;
                    sum += value;
                    sumsquared += value * value;

                    try
                    {
                        sd = Math.Sqrt((sumsquared - (sum * sum / count)) / (count - 1));
                    }
                    catch
                    {
                        sd = 0;
                    }

                    try
                    {
                        mean = sum / count;
                    }
                    catch
                    {
                        mean = 0;
                    }

                    if (sd > maxsd)
                    {
                        count = 0;
                        sum = 0;
                        sumsquared = 0;
                        min = 0;
                        max = 0;
                        Log.LogMessage("Standard deviation in pings was too high (" + sd + ", received ping: " + value + "), reset counters");
                    }
                }
            }
        }

        const int PING = 2;
        const int PONG = 3;
        const int START = 4;
        public event StatusUpdateEventHandler PingUpdate;
        private PingT ping;

        protected virtual void OnPingUpdate()
        {
            if (PingUpdate != null)
                PingUpdate();
        }

        public uint GetPing()
        {
            return ping.Ping;
        }

        private void SendTimestamp(UdpClient socket, IPEndPoint endpoint, int command = PING)
        {
            var timestamp = DateTime.Now.Ticks;
            var buffer = new byte[4 + 8];
            Buffer.BlockCopy(BitConverter.GetBytes(command), 0, buffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(timestamp), 0, buffer, 4, 8);
            socket.Send(buffer, buffer.Length, endpoint);
        }

        private uint GetPingFromData(byte[] data, int offset = 4)
        {
            var timestamp = BitConverter.ToInt64(data, offset);
            var timespan = new System.TimeSpan(DateTime.Now.Ticks - timestamp);
            return (uint)timespan.Milliseconds;
        }

        private void ReplyToPing(UdpClient socket, IPEndPoint endpoint, byte[] data)
        {
            var buffer = new byte[4 + 8];
            Buffer.BlockCopy(BitConverter.GetBytes(PONG), 0, buffer, 0, 4);
            Buffer.BlockCopy(data, 0, buffer, 4, 8);
            socket.Send(buffer, buffer.Length, endpoint);
        }

        private void SetupStartTimer(int interval = 5000)
        {
            var timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnStartTimerExpired);
            timer.Interval = interval;
            timer.Enabled = true;
            timer.AutoReset = false;
        }

        private static void OnStartTimerExpired(object source, ElapsedEventArgs e)
        {
            Program.SCWindow.SendReplayStart();
        }
    }
}
