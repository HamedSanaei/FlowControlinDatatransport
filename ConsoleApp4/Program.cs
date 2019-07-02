using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Data size (bit):");
            ulong DatatoSend = Convert.ToUInt64 (Console.ReadLine());

            Console.WriteLine("Enter bit rate (bps):");
            ulong B = Convert.ToUInt64(Console.ReadLine());

            Console.WriteLine("Enter distance (meter):");
            ulong Distance = Convert.ToUInt64(Console.ReadLine());

            Console.WriteLine("Enter signal speed (mps):");
            ulong SignalSpeed = Convert.ToUInt64(Console.ReadLine());

            Console.WriteLine("Enter Frame size (bit):");
            ulong FrameSize = Convert.ToUInt64(Console.ReadLine());

            Node nodeA = new Node() {FrameSize = FrameSize, TransmissionRate = B , DatatoSend = DatatoSend,Distance=Distance,SignalSpeed=SignalSpeed};
            Node nodeB = new Node(nodeA,B,B,0,0,DatatoSend);


            Console.WriteLine("============================================");
            Console.WriteLine("Stop&Wait protocol:");

            Console.WriteLine("Utlization is:"+nodeA.CalUtlization());
            Console.WriteLine("Bit length of link is:" + nodeA.CalBitLength() +"bit");

            nodeA.StartFrameSending();

            
            Console.ReadLine();
        }
    }
}
