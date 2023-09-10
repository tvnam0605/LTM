using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
      

        static void Main(string[] args)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Dang ket noi voi server...");
            try
            {
                serverSocket.Connect(serverEndPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Khong the ket noi den server");
                Console.ReadKey();
                return;
            }
            if (serverSocket.Connected)
            {
                byte[] buff = new byte[1024];
                Console.WriteLine("Ket noi thanh cong voi server ...");
                var byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);
            }

            while (true)
            {
                byte[] buff = new byte[1024];
                string str = Console.ReadLine();
                buff = Encoding.ASCII.GetBytes(str);
                serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);
                var byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);
            }
            Console.ReadKey();
        }
    }
}
