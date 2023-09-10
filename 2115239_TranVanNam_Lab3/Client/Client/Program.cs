using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
	class RetryUdpClient
	{
		private byte[] data;
		private EndPoint Remote;

		public RetryUdpClient()
		{
			string input, stringData;
			int recv;
			IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
			Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			int sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			Console.WriteLine("Gia tri timeout mac dinh: {0}", sockopt);

			server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
			sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
			Console.WriteLine("Gia tri timeout moi: {0}", sockopt);

			string welcome = "Xin chao server";
			data = Encoding.ASCII.GetBytes(welcome);
			recv = SndRcvData(server, data, ipep);

			if (recv > 0)
			{
				stringData = Encoding.ASCII.GetString(data, 0, recv);
				Console.WriteLine(stringData);
			}
			else
			{
				Console.WriteLine("Khong the lien lac voi thiet bi o xa");
				return;
			}

			// Khởi tạo biến Remote bên ngoài vòng lặp
			Remote = new IPEndPoint(IPAddress.Any, 0);

			while (true)
			{
				input = Console.ReadLine();

				if (input == "exit")
					break;

				recv = SndRcvData(server, Encoding.ASCII.GetBytes(input), ipep);

				if (recv > 0)
				{
					stringData = Encoding.ASCII.GetString(data, 0, recv);
					Console.WriteLine(stringData);
				}
				else
				{
					Console.WriteLine("Khong nhan duoc cau tra loi");
				}
			}

			Console.WriteLine("Dang dong client");
			server.Close();
		}
		private int SndRcvData(Socket s, byte[] message, EndPoint rmtdevice)
		{
			int recv;
			int retry = 0;

			while (true)
			{
				Console.WriteLine("Truyen lai lan thu: #{0}", retry);

				try
				{
					s.SendTo(message, message.Length, SocketFlags.None, rmtdevice);
					data = new byte[1024];
					recv = s.ReceiveFrom(data, ref rmtdevice); // Sử dụng rmtdevice đã truyền vào
				}
				catch (SocketException)
				{
					recv = 0;
				}

				if (recv > 0)
				{
					// Dữ liệu đã được nhận thành công
					return recv;
				}
				else
				{
					// Gửi lại dữ liệu nếu không nhận được phản hồi và retry chưa vượt quá 4 lần
					if (retry >= 4)
					{
						return 0;
					}

					retry++; // Tăng giá trị của retry
				}
			}
		}



		class Program
		{
			static void Main(string[] args)
			{
				RetryUdpClient client = new RetryUdpClient();
			}
		}
	}
}




