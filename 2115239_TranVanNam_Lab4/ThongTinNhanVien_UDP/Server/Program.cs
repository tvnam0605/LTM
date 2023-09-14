using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Share;

namespace Server
{
	class Program
	{
		static void Main(string[] args)
		{
			int port = 9050;
			UdpClient udpServer = new UdpClient(port);
			Console.WriteLine("Server dang nghe tren cong {0}...", port);

			while (true)
			{
				IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, port);
				byte[] data = udpServer.Receive(ref clientEndPoint);
				Employee emp1 = new Employee(data);
				Console.WriteLine("Du lieu nhan tu client:");
				Console.WriteLine("emp1.EmployeeID = {0}", emp1.EmployeeID);
				Console.WriteLine("emp1.LastName = {0}", emp1.LastName);
				Console.WriteLine("emp1.FirstName = {0}", emp1.FirstName);
				Console.WriteLine("emp1.YearsService = {0}", emp1.YearsService);
				Console.WriteLine("emp1.Salary = {0}", emp1.Salary);

				// Ghi dữ liệu vào tệp văn bản
				WriteToTextFile(emp1);
			}
		}

		// Phương thức để ghi dữ liệu vào tệp văn bản
		static void WriteToTextFile(Employee employee)
		{
			try
			{
				string fileName = @"D:\LapTrinhMang\2115239_TranVanNam_Lab4\ThongTinNhanVien_UDP\Server\employee_data.txt"; // Đặt đường dẫn tệp văn bản tại đây
				using (StreamWriter writer = new StreamWriter(fileName, true))
				{
					// Ghi dữ liệu vào tệp văn bản
					writer.WriteLine("EmployeeID: " + employee.EmployeeID);
					writer.WriteLine("LastName: " + employee.LastName);
					writer.WriteLine("FirstName: " + employee.FirstName);
					writer.WriteLine("YearsService: " + employee.YearsService);
					writer.WriteLine("Salary: " + employee.Salary);
					writer.WriteLine("------------------------------");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Lỗi khi ghi tệp văn bản: " + ex.Message);
			}
		}
	}
}
