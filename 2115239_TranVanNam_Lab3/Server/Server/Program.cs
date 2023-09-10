using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			// Tạo địa chỉ IP và cổng cho server
			IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

			// Tạo socket UDP
			Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			// Bind socket với địa chỉ IP và cổng
			serverSocket.Bind(serverEndPoint);

			Console.WriteLine("Dang cho client ket noi den...");

			// Tạo một luồng con để giả lập chậm trễ
			Thread slowServerThread = new Thread(() =>
			{
				while (true)
				{
					// Ngừng máy chủ trong 5 giây để giả lập trường hợp máy chủ chậm trễ
					Thread.Sleep(5000);
				}
			});

			// Bắt đầu luồng giả lập chậm trễ
			slowServerThread.Start();

			while (true)
			{
				byte[] receiveBuffer = new byte[1024];
				EndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);

				// Nhận dữ liệu từ client
				int bytesRead = serverSocket.ReceiveFrom(receiveBuffer, ref clientEndPoint);

				// Chuyển dữ liệu nhận được thành chuỗi
				string clientData = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
				Console.WriteLine($"Thong diep duoc nhan tu {clientEndPoint}: {clientData}");

				// Phản hồi client
				string response = "Server da nhan duoc tin nhan cua ban: " + clientData;
				byte[] sendBuffer = Encoding.ASCII.GetBytes(response);
				serverSocket.SendTo(sendBuffer, clientEndPoint);
			}
		}
	}
}
