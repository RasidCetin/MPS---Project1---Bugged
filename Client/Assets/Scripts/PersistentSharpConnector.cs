using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentSharpConnector : MonoBehaviour
{
	SharpClient conn;

	void Awake()
	{
		// Stops object from automatically destroyed on loading a scene
		DontDestroyOnLoad(this);
	}

	// Use this for initialization
	void Start ()
	{
		conn = new SharpClient();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	// called when the "join game" button is pressed
	public bool ConnectToServer(string ip, int port)
	{
		return conn.Connect(ip, port);
	}

	public void SendToServer(string payload)
	{
		conn.Send(payload);
	}

	public List<string> ReceveFromServer()
	{
		return conn.Rececive();
	}

	void OnApplicationQuit()
	{
		try
		{
			conn.Disconnect();
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
}
