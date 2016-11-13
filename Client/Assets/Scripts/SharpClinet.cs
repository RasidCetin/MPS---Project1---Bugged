using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;

public class SharpClient
{
	private static System.Threading.ManualResetEvent connectDone =
			new System.Threading.ManualResetEvent(false);

	TcpClient mTcpClient;
	byte[] mRx;
	const int READ_BUFFER_SIZE = 2048;
	bool error;
    /* Salvam toate mesajele care vin asincron
    Mesajele sunt verificate la fiecare Update() */
	List<string> response;

	public SharpClient()
	{
        response = new List<string>();
		error = false;
	}

	public bool Connect(string ip, int port)
	{
		error = false;

		try
		{
			IPAddress ipa;

			if (!IPAddress.TryParse(ip, out ipa))
			{
				Console.WriteLine("Wrong IP adress");
				return false;
			}


			// The TcpClient is a subclass of Socket, providing higher level 
			// functionality like streaming.
			mTcpClient = new TcpClient();

			connectDone.Reset();
			mTcpClient.BeginConnect(ipa, port, onCompleteConnect, mTcpClient);

			// Wait here until the callback processes the connection.
			connectDone.WaitOne(2000);

			return !error;
		}
		catch (Exception ex)
		{
			Console.WriteLine("Connect:");
			Console.WriteLine(ex.Message);
			return false;
		}
	}

	void onCompleteConnect(IAsyncResult iar)
	{
		TcpClient tcpc;

		try
		{
			tcpc = (TcpClient)iar.AsyncState;
			tcpc.EndConnect(iar);
			mRx = new byte[READ_BUFFER_SIZE];
			tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);
		}
		catch (Exception exc)
		{
			Console.WriteLine("onCompleteConnect:");
			Console.WriteLine(exc.Message);
			error = true;
		}
	}

	void onCompleteReadFromServerStream(IAsyncResult iar)
	{
		TcpClient tcpc;
		int nCountBytesReceivedFromServer;
		string strReceived;

		try
		{
			tcpc = (TcpClient)iar.AsyncState;
			nCountBytesReceivedFromServer = tcpc.GetStream().EndRead(iar);

			if (nCountBytesReceivedFromServer == 0)
			{
				Console.WriteLine("Connection broken.");
				return;
			}
			strReceived = Encoding.ASCII.GetString(mRx, 0, nCountBytesReceivedFromServer);

  
            lock(response)
            {
                response.Add(strReceived);
            }

			Console.WriteLine(strReceived);
            



			mRx = new byte[READ_BUFFER_SIZE];
			tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);

		}
		catch (Exception exc)
		{
			Console.WriteLine("onCompleteReadFromServerStream:");
			Console.WriteLine(exc.Message);
			error = true;
		}
	}

	public bool Send(string payload)
	{
		error = false;

		try
		{
			if (mTcpClient != null)
			{
				byte[] tx = Encoding.ASCII.GetBytes(payload);

				if (mTcpClient.Client.Connected)
				{
					mTcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTcpClient);
					return true;
				}
			}

			return false;
		}
		catch (Exception exc)
		{
			Console.WriteLine("Send:");
			Console.WriteLine(exc.Message);
			error = true;
			return false;
		}
	}

	void onCompleteWriteToServer(IAsyncResult iar)
	{
		TcpClient tcpc;

		try
		{
			tcpc = (TcpClient)iar.AsyncState;
			tcpc.GetStream().EndWrite(iar);
		}
		catch (Exception exc)
		{
			Console.WriteLine("onCompleteWriteToServer:");
			Console.WriteLine(exc.Message);
			error = true;
		}
	}

	public List<string> Rececive()
	{
        List<string> r;
        lock (response)
        {
            r = response;
            response = new List<string>();
        }
		

		return r;
	}

	public void Disconnect()
	{
		Send("Disconnect");
	}
}
