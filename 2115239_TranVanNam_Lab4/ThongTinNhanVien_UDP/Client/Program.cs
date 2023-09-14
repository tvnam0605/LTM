using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Share;
namespace Client
{
	class Program
	{
		static void Main()
		{
			while (true)
			{
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

				UdpClient udpClient = new UdpClient();
				IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);

				byte[] data = emp1.GetBytes();
				udpClient.Send(data, data.Length, serverEndPoint);
				Console.WriteLine("Data sent to server.");

				udpClient.Close();

				Console.Write("Ban co muon nhap tiep khong? (Khong de ket thuc, bat ky de nhap tiep): ");
				string continueInput = Console.ReadLine();
				if (continueInput.ToLower() == "khong")
				{
					break;
				}
			}
		}
	}
}
