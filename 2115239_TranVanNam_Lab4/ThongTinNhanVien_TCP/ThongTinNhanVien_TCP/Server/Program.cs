using System;
using System.Collections.Generic;
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
			byte[] data = new byte[1024];
			TcpListener server = new TcpListener(IPAddress.Any, 9050);
			server.Start();
			TcpClient client = server.AcceptTcpClient();
			NetworkStream ns = client.GetStream();

			byte[] size = new byte[2];
			int recv = ns.Read(size, 0, 2);
			int packsize = BitConverter.ToInt16(size, 0);
			Console.WriteLine("Kich thuoc goi tin = {0}", packsize);
			recv = ns.Read(data, 0, packsize);
			Employee emp1 = new Employee(data);
			Console.WriteLine("emp1.EmployeeID = {0}", emp1.EmployeeID);
			Console.WriteLine("emp1.LastName = {0}", emp1.LastName);
			Console.WriteLine("emp1.FirstName = {0}", emp1.FirstName);
			Console.WriteLine("emp1.YearsService = {0}", emp1.YearsService);
			Console.WriteLine("emp1.Salary = {0}", emp1.Salary);

			// Ghi dữ liệu vào tệp văn bản
			WriteToTextFile(emp1);

			ns.Close();
			client.Close();
			server.Stop();
			Console.ReadKey();
		}
		static void WriteToTextFile(Employee employee)
		{
			string fileName = @"D:\LapTrinhMang\2115239_TranVanNam_Lab4\ThongTinNhanVien_TCP\ThongTinNhanVien_TCP\Server\employee_data.txt";
			try
			{
				using (StreamWriter writer = new StreamWriter(fileName, true))
				{
					
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
