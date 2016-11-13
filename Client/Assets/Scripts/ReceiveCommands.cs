using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class ReceiveCommands : MonoBehaviour {

    public PersistentSharpConnector conn;
	public PersistentInitGameData initGameData;
    public PlayerController playerController;
    public WorldGeneration gen;

	bool receivedLabyrinth;

	// Use this for initialization
	void Start ()
	{
        GameObject obj = GameObject.FindGameObjectWithTag("WorldGeneration") as GameObject;
        gen = obj.GetComponent<WorldGeneration>();
        playerController = new PlayerController(gen);
       
        if (gen == null)
            Debug.Log("GEN E NULL");
        receivedLabyrinth = false;

	}
	

    void treatCommand(List<string> receivedPayload)
    {
        foreach (string command in receivedPayload)
        {
            if (command == "Start")
            {
                // received the start game command
                UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }
            else if (command[1] == '2') // M2 -> primesc matrice
            {
                // received the labyrinth
                initGameData.CreateLabyrinth(command.Substring(2));
                conn.SendToServer("Done creating labyrinth");
                Debug.Log("Pasul 2");
            }
            else if (command[1] == '1') // M1 -> primesc index
            {
                playerController.myId = Int32.Parse(command.Substring(2));
                Debug.Log("Pasul 1");
            }
            else if (command[1] == '3') // M3 -> primesc pozitiile initiale
            {
                string data = command.Substring(2);
                string[] positions = data.Split('|');

                for (int i = 0; i < positions.Length; ++i)
                {
                    string[] coord = positions[i].Split(',');
                    Vector2 initPos = new Vector2(Int32.Parse(coord[0]), Int32.Parse(coord[1]));
                    PlayerController.Player p = new PlayerController.Player(i);
                    playerController.placeOnTile(p, initPos);
                    playerController.players.Add(p);
                }
                conn.SendToServer("Done positioning");
                Debug.Log("Pasul 3");
            }


            /*if (initGameData.DoneCreatingLabyrinth())
            {
                
            }*/

        }
    }
	// Update is called once per frame
	void Update ()
	{
        if (conn == null)
        {
            return;
        }

        List<string> receivedPayload;
        receivedPayload = conn.ReceveFromServer();

        if (receivedPayload.Count == 0)
        {
            // doesn't receive anything
            return;
        }

        playerController.treatInput();
        treatCommand(receivedPayload);
        

		
		
        
	}
}
