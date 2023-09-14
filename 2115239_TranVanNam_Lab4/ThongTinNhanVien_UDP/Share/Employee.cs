using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share
{
	public class Employee
	{
		public int EmployeeID;
		public string LastName;
		public string FirstName;
		public int YearsService;
		public double Salary;
		public int size;

		public Employee()
		{

		}

		public Employee(byte[] data)
		{

			int place = 0;
			EmployeeID = BitConverter.ToInt32(data, place);
			place += 4;
			int lastNameSize = BitConverter.ToInt32(data, place);
			place += 4;
			LastName = Encoding.ASCII.GetString(data, place, lastNameSize);
			place += lastNameSize;
			int firstNameSize = BitConverter.ToInt32(data, place);
			place += 4;
			FirstName = Encoding.ASCII.GetString(data, place, firstNameSize);
			place += firstNameSize;
			YearsService = BitConverter.ToInt32(data, place);
			place += 4;
			Salary = BitConverter.ToDouble(data, place);
		}

		public byte[] GetBytes()
		{
			byte[] data = new byte[1024];
			int place = 0;
			Buffer.BlockCopy(BitConverter.GetBytes(EmployeeID), 0, data, place, 4);
			place += 4;
			byte[] lastNameBytes = Encoding.ASCII.GetBytes(LastName);
			Buffer.BlockCopy(BitConverter.GetBytes(lastNameBytes.Length), 0, data, place, 4);
			place += 4;
			Buffer.BlockCopy(lastNameBytes, 0, data, place, lastNameBytes.Length);
			place += lastNameBytes.Length;
			byte[] firstNameBytes = Encoding.ASCII.GetBytes(FirstName);
			Buffer.BlockCopy(BitConverter.GetBytes(firstNameBytes.Length), 0, data, place, 4);
			place += 4;
			Buffer.BlockCopy(firstNameBytes, 0, data, place, firstNameBytes.Length);
			place += firstNameBytes.Length;
			Buffer.BlockCopy(BitConverter.GetBytes(YearsService), 0, data, place, 4);
			place += 4;
			Buffer.BlockCopy(BitConverter.GetBytes(Salary), 0, data, place, 8);
			place += 8;
			size = place;

			return data;
		}
	}
}
