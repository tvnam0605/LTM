using System;
using System.Net.Sockets;
using System.Text;

namespace TcpEchoClient
{
	class TcpEchoClient
	{
		static void Main(string[] args)
		{
			if (args.Length != 3)
			{
				Console.WriteLine("Sử dụng: TcpEchoClient <địa chỉ máy chủ> <port> <nội dung gửi đến máy chủ>");
				return;
			}

			string serverAddress = args[0];
			int serverPort = Int32.Parse(args[1]);
			string messageToSend = args[2];

			try
			{
				// Tạo đối tượng TcpClient để kết nối đến server
				using (TcpClient client = new TcpClient(serverAddress, serverPort))
				{
					Console.WriteLine("Ket noi den " + serverAddress + ":" + serverPort);

					// Lấy luồng dữ liệu để gửi và nhận dữ liệu
					using (NetworkStream networkStream = client.GetStream())
					{
						// Chuyển đổi chuỗi thành mảng byte để gửi đi
						byte[] sendData = Encoding.UTF8.GetBytes(messageToSend);

						// Gửi dữ liệu đến server
						networkStream.Write(sendData, 0, sendData.Length);

						Console.WriteLine("Da gui: " + messageToSend);

						// Đọc dữ liệu phản hồi từ server
						byte[] receiveData = new byte[client.ReceiveBufferSize];
						int bytesRead = networkStream.Read(receiveData, 0, receiveData.Length);
						string responseMessage = Encoding.UTF8.GetString(receiveData, 0, bytesRead);

						Console.WriteLine("Phan hoi tu server: " + responseMessage);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Lỗi: " + ex.Message);
			}
		}
	}
}
