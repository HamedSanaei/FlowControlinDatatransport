using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ConsoleApp4
{
    
    class Node
    {
        public Node Neighbor { get; set; }
        public ulong TransmissionRate { get; set; }
        public ulong RecieveRate { get; set; }
        public ulong MaxTransmissionRate { get; set; }
        public ulong MaxRecieveRate { get; set; }
        public ulong FrameSize { get; set; }
        public ulong DatatoSend { get; set; }
        public TimeSpan Tack { get; set; }
        public bool FrameLost { get; set; }

        private static System.Timers.Timer aTimer;
        private static Timer GTimer;

        public int FrameNO { get; set; }

        public ulong GetDatatoSend()
        {
            return DatatoSend;
        }

        public void SetDatatoSend(ulong value)
        {
            DatatoSend = value;
        }

        public ulong Distance { get; set; }
        public ulong SignalSpeed { get; set; }

        public Node(Node neighbor, ulong trasmissonrate, ulong receiverate, ulong maxtransmissionrate, ulong maxreceiverate, ulong datatoSend)

        {
            Neighbor = neighbor;
            neighbor.Neighbor = this;

            TransmissionRate = trasmissonrate;
            RecieveRate = receiverate;
            MaxTransmissionRate = maxreceiverate;
            MaxRecieveRate = MaxRecieveRate;
            DatatoSend = datatoSend;
            FrameNO = 0;
            FrameLost = true;
        }

        public Node()
        {
            FrameNO = 0;
            FrameLost = true;
        }

        double CalUtlization(TimeSpan tpropagation,TimeSpan tframe )
        {
            var U = (tframe.TotalSeconds) / ((2 * tpropagation.TotalSeconds) + tframe.TotalSeconds);
            return U;
        }

        internal double CalUtlization()
        {
            TimeSpan Tpropagation = new TimeSpan(Convert.ToInt64((Distance / SignalSpeed) * 10000000));
            TimeSpan Tframe = new TimeSpan(Convert.ToInt64( (Convert.ToDouble(FrameSize)/ Convert.ToDouble( TransmissionRate)) * 10000000));
            return CalUtlization(Tpropagation, Tframe);
        }

        internal long CalBitLength()
        {
            return Convert.ToInt64(TransmissionRate * (Distance / SignalSpeed));
        }


        internal void StartFrameSending()
        {
            
            

            TimeSpan Tpropagation = new TimeSpan(Convert.ToInt64((Distance / SignalSpeed) * 10000000));
            TimeSpan Tframe = new TimeSpan(Convert.ToInt64((Convert.ToDouble(FrameSize) / Convert.ToDouble(TransmissionRate)) * 10000000));

            TimeSpan Tackn = Tpropagation + Tpropagation + Tframe;
            this.Tack = Tackn;
            
            aTimer = new System.Timers.Timer(Tack.TotalMilliseconds);
            aTimer.Elapsed += OnTimedEvent ;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

          
         
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            int framenumber = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(DatatoSend / FrameSize)));
            if (FrameNO%12 ==0 && FrameLost == true)
            {
                Console.WriteLine("packetNo:" + FrameNO + " Sent At:" + DateTime.Now.ToLongTimeString() + " Ack At:" + "No Ack!");
                FrameLost = false;
            }
            else if (FrameNO % 12 == 0 && FrameLost == false)
            {
                Console.WriteLine("packetNo:" + FrameNO + " Sent At:" + DateTime.Now.ToLongTimeString() + " Ack At:" + (DateTime.Now + Tack).ToLongTimeString()+"(Second Try!)" );
                this.FrameNO++;
                FrameLost = true;
            }
            else
            {
                Console.WriteLine("packetNo:" + FrameNO + " Sent At:" + DateTime.Now.ToLongTimeString() + " Ack At:" + (DateTime.Now + Tack).ToLongTimeString());
                this.FrameNO++;
            }

            
            if (framenumber <= FrameNO)
            {
                aTimer.Enabled = false;
                aTimer.Stop();
                Console.WriteLine("sent!");
            }

        }


        internal void StartFrameSendingGobackN()
        {

        }
        

    }
}
