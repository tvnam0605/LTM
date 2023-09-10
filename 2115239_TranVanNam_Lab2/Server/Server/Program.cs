using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        private static int byteReceive;
        private static string str;

        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(10);
            Socket clientSocket = serverSocket.Accept();
            EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
            Console.WriteLine(clientEndPoint.ToString());
            byte[] buff;
            string hello = "Hello Client";
            buff = Encoding.ASCII.GetBytes(hello);
            clientSocket.Send(buff, 0, buff.Length, SocketFlags.None);
            while (true)
            {
                buff = new byte[1024];
                byteReceive = clientSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);
                clientSocket.Send(buff, 0, byteReceive, SocketFlags.None);
            }


            Console.ReadKey();

        }
    }
}
