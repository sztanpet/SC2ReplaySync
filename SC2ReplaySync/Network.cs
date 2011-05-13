using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace SC2ReplaySync
{
    
    public partial class Network
    {
        public struct PingT
        {
            // the max standard deviation we want to handle
            private const double maxsd = 100;
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

                    if (count != 0 && count - 1 != 0)
                        sd = Math.Sqrt((sumsquared - (sum * sum / count)) / (count - 1));
                    else
                        sd = 0;
                    
                    if (count != 0)
                        mean = sum / count;
                    else
                        mean = 0;

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

        public event PingUpdateEventHandler PingUpdate;

        protected PingT ping;
        protected System.Timers.Timer pingtimer;
        protected bool pingtimercancelled = false;
        protected Thread thread;
        protected UdpClient socket;
        protected Stopwatch stopwatch;

        protected virtual void OnPingUpdate(uint ping)
        {
            if (PingUpdate != null)
                PingUpdate(ping);
        }

        protected virtual void Start(object arg)
        {
        }

        protected virtual void Run(object arg)
        {
            try
            {
                Start(arg);
            }
            catch (ThreadAbortException)
            {
                Stop();
                
            }
            Log.LogMessage("stop");
        }

        public bool ThreadIsAlive()
        {
            if (thread == null)
                return false;
            else
                return thread.IsAlive;
        }

        public virtual void StopThread()
        {
            if (thread == null || !thread.IsAlive)
                return;

            try
            {
                pingtimercancelled = true;
                thread.Abort();
                Stop();
                thread.Join();
            }
            catch { }
        }

        public virtual void Stop()
        {
            try
            {
                Program.GUI.StartReplay -= new StartReplayEventHandler(SetupStartTimer);
                pingtimercancelled = true;
                pingtimer.Enabled = false;
                stopwatch.Stop();
                stopwatch = null;
                pingtimer.Dispose();
                socket.Close();
            }
            catch { }
        }

        public uint GetPing()
        {
            return ping.Ping;
        }

        protected void SendTimestamp(IPEndPoint endpoint, int command = PING)
        {
            if (stopwatch == null)
            {
                stopwatch = Stopwatch.StartNew();
            }
            
            var timestamp = stopwatch.ElapsedTicks;
            var buffer = new byte[4 + 8];
            Buffer.BlockCopy(BitConverter.GetBytes(command), 0, buffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(timestamp), 0, buffer, 4, 8);
            socket.Send(buffer, buffer.Length, endpoint);
        }

        protected uint GetPingFromData(byte[] data, int offset = 4)
        {
            if (stopwatch == null)
                return 0;
            
            var timestamp = stopwatch.ElapsedTicks;
            var clienttimestamp = BitConverter.ToInt64(data, offset);
            var miliseconds = ((timestamp - clienttimestamp) / Math.Round((double)(Stopwatch.Frequency / 1000)));
            
            return (uint)miliseconds;
        }

        protected void ReplyToPing(IPEndPoint endpoint, byte[] data)
        {
            Buffer.BlockCopy(BitConverter.GetBytes(PONG), 0, data, 0, 4);
            socket.Send(data, data.Length, endpoint);
        }

        protected virtual void SetupStartTimer(int startafter = 5)
        {
            if (stopwatch == null)
            {
                stopwatch = Stopwatch.StartNew();
            }

            var timestamp = stopwatch.ElapsedTicks;
            var buffer = new byte[4 + 8];
            Buffer.BlockCopy(BitConverter.GetBytes(START), 0, buffer, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(timestamp), 0, buffer, 4, 8);
            socket.Send(buffer, buffer.Length);

            StartTimer(startafter * 1000);
        }

        protected void StartTimer(int startafter)
        {
            var timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(OnStartTimerExpired);
            timer.Interval = startafter;
            timer.Enabled = true;
            timer.AutoReset = false;
        }
        
        protected virtual void OnStartTimerExpired(object source, ElapsedEventArgs e)
        {
            Program.SCWindow.SendReplayStart();
        }
    }
}
