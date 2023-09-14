using System;
using System.Net.Sockets;
using Share;

namespace Client
{
	class Program
	{
		static void Main()
		{
			TcpClient client;
			try
			{
				client = new TcpClient("127.0.0.1", 9050);
			}
			catch (SocketException)
			{
				Console.WriteLine("Khong ket noi duoc voi server.");
				return;
			}
			NetworkStream ns = client.GetStream();

			while (true)
			{
				// Nhap vao tu ban phim
				Employee emp1 = new Employee();
				Console.Write("Nhap EmployeeID: ");
				emp1.EmployeeID = int.Parse(Console.ReadLine());

				Console.Write("Nhap LastName: ");
				emp1.LastName = Console.ReadLine();

				Console.Write("Nhap FirstName: ");
				emp1.FirstName = Console.ReadLine();

				Console.Write("Nhap YearsService: ");
				emp1.YearsService = int.Parse(Console.ReadLine());

				Console.Write("Nhap Salary: ");
				emp1.Salary = double.Parse(Console.ReadLine());

				// Chuyển đối tượng Employee thành mảng byte và gửi lên server
				byte[] data = emp1.GetBytes();
				int size = emp1.size;
				byte[] packsize = new byte[2];
				packsize = BitConverter.GetBytes((short)size);
				ns.Write(packsize, 0, 2);
				ns.Write(data, 0, size);
				ns.Flush();

				// Hỏi người dùng có tiếp tục không
				Console.Write("Ban co muon nhap them du lieu khong? (Nhap 'Khong' de thoat): ");
				string response = Console.ReadLine();
				if (response.Equals("Khong", StringComparison.OrdinalIgnoreCase))
					break;
			}

			ns.Close();
			client.Close();
			Console.WriteLine("Da dong ket noi.");
		}
	}
}
