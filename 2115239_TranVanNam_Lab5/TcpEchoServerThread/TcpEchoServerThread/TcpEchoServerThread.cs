using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IProtocol; // Import các namespace và lớp từ dự án IProtocol

namespace TcpEchoServerThread
{
	class TcpEchoServerThread
	{
		static void Main(string[] args)
		{
			if (args.Length != 1)
			{
				Console.WriteLine("Sử dụng: TcpEchoServerThread <Port>");
				return;
			}

			int serverPort = Int32.Parse(args[0]);
			TcpListener listener = new TcpListener(IPAddress.Any, serverPort);
			ILogger logger = new ConsoleLogger(); // Sử dụng ConsoleLogger để ghi log
			listener.Start();

			Console.WriteLine("Server dang lang nghe tren cong " + serverPort);

			for (; ; )
			{
				try
				{
					Socket client = listener.AcceptSocket();
					EchoProtocol protocol = new EchoProtocol(client, logger);
					Thread thread = new Thread(new ThreadStart(protocol.handeclient));
					thread.Start();
					logger.writeEntry("Tao va bat dau Thread = " + thread.GetHashCode());
				}
				catch (System.IO.IOException e)
				{
					logger.writeEntry("Lỗi: " + e.Message);
				}
			}
		}
	}
}
