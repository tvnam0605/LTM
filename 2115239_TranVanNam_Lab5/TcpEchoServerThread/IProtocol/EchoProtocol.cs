using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IProtocol
{
	public class EchoProtocol : IProtocol
	{
		public const int BUFSIZE = 32; // byte size of IO buffer
		private Socket clntSock; // Connection socket
		private ILogger logger; // Logging facility
		public EchoProtocol(Socket clntSock, ILogger logger)
		{
			this.clntSock = clntSock;
			this.logger = logger;
		}

		public void handeclient()
		{
			ArrayList entry = new ArrayList();
			entry.Add("Client address and port = " + clntSock.RemoteEndPoint);
			entry.Add("Thread = " + Thread.CurrentThread.GetHashCode());
			try
			{
				int recvMsgSize;
				int totalBytesEchoed = 0;
				byte[] rcvBuffer = new byte[BUFSIZE];
				try
				{
					while ((recvMsgSize = clntSock.Receive(rcvBuffer, 0, rcvBuffer.Length, SocketFlags.None)) > 0)
					{
						clntSock.Send(rcvBuffer, 0, recvMsgSize, SocketFlags.None);
						totalBytesEchoed += recvMsgSize;
					}
				}
				catch (SocketException se)
				{

					entry.Add(se.ErrorCode + " : " + se.Message);
				}
				entry.Add("Client finished; echoed " + totalBytesEchoed + "bytes.");


			}
			catch (SocketException se)
			{

				entry.Add(se.ErrorCode + " : " + se.Message);
			}
			clntSock.Close();
			logger.writeEntry(entry);
		}


	}
}
