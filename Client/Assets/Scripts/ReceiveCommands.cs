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

    public Sprite jekyllSprite;
    public Sprite hydeSprite;

    // Use this for initialization
    void Start ()
	{
        playerController = new PlayerController();
       
        if (gen == null)
            Debug.Log("GEN E NULL");
        receivedLabyrinth = false;

        PlayerController.jekyllSprite = jekyllSprite;
        PlayerController.hydeSprite = hydeSprite;

    }

    void Awake()
    {
        // make this object persistent
        DontDestroyOnLoad(this);
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
                receivedLabyrinth = true;
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
                Debug.Log(data);

                for (int i = 0; i < positions.Length - 1; ++i)
                {
                    string[] coord = positions[i].Split(',');
                    Debug.Log(positions[i]);
                    Debug.Log(coord[0] + " " + coord[1]);
                    Vector2 initPos = new Vector2(Int32.Parse(coord[0]), Int32.Parse(coord[1]));
                    PlayerController.Player p = new PlayerController.Player(i);
                    //playerController.placeOnTile(p, initPos);
                    p.pos = initPos;
                    playerController.players.Add(p);
                }
                conn.SendToServer("Done positioning");
                Debug.Log("Pasul 3");
            }
            else if (command[1] == '4')
            {
                string data = command.Substring(2);
                string[] datas = data.Split(',');
                int id = Int32.Parse(datas[0]);
                string[] pos = datas[1].Split(';');
                string[] crt_dir = datas[2].Split(';');
                string[] next_dir = datas[3].Split(';');
                string[] turn_point = datas[4].Split(';');


                playerController.players[id].pos = new Vector2(float.Parse(pos[0]), float.Parse(pos[1]));
                playerController.players[id].crt_dir = new Vector2(float.Parse(crt_dir[0]), float.Parse(crt_dir[1]));
                playerController.players[id].next_dir = new Vector2(float.Parse(next_dir[0]), float.Parse(next_dir[1]));
                playerController.players[id].turn_point = new Vector2(float.Parse(turn_point[0]), float.Parse(turn_point[1]));

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
            //return;
        }
        Debug.Log("controller gen: " + playerController.gen);


        treatCommand(receivedPayload);
        if (playerController.gen != null)
        {

            //Debug.Log("treating input....");
            playerController.treatInput();
            playerController.simulate();
        }

    }


}
