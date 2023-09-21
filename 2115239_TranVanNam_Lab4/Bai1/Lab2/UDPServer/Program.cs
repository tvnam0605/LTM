using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9000);
            UdpClient newsock = new UdpClient(ipep);
            Console.WriteLine("Waiting for a client...");
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            data = newsock.Receive(ref sender);
            Console.WriteLine("Message received from {0}: ", sender.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data,0,data.Length));
            string welcome = "Welcome to my test server";
            data = Encoding.ASCII.GetBytes(welcome);
            newsock.Send(data, data.Length, sender);
            while (true)
            {
                data= newsock.Receive(ref sender);
                Console.WriteLine(Encoding.ASCII.GetString(data,0, data.Length));
                newsock.Send(data, data.Length, sender);
            }
        }
    }
}
