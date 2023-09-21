using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            string input, stringData;
            UdpClient server = new UdpClient("127.0.0.1", 9000);
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            string welcome = "Hello, are you there?";
            data = Encoding.ASCII.GetBytes(welcome);
            server.Send(data, data.Length);
            data = server.Receive(ref sender);
            Console.WriteLine("Message received from {0}:", sender.ToString());
            stringData = Encoding.ASCII.GetString(data, 0, data.Length);
            Console.WriteLine(stringData);
            while(true)
            {
                input = Console.ReadLine();
                if(input == "exit") break;

                server.Send(Encoding.ASCII.GetBytes(input), input.Length);
                data = server.Receive(ref sender);
                stringData = Encoding.ASCII.GetString(data, 0, data.Length);
                Console.WriteLine(stringData);
            }
            Console.WriteLine("Stopping client");
            server.Close();
        }
    }
}
