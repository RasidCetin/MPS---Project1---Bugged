using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeLevel : MonoBehaviour
{
	public Text serverIpAddress;
	public Text userName;

	public PersistentSharpConnector conn;

    public GameObject canvasMain;
    public GameObject canvasWaiting;

	// Use this for initialization
	void Start () {
        canvasWaiting.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void startGame()
    {
		if (conn.ConnectToServer(serverIpAddress.text, 2737))
		{
            Debug.Log("Connect to server succeeded!");
            canvasMain.SetActive(false);
            canvasWaiting.SetActive(true);
		}
		else
		{
			Debug.Log("Connect to server FAILED! :(");
		}
    }
}
