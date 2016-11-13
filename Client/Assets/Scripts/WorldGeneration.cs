using UnityEngine;
using System.Collections;

public class WorldGeneration : MonoBehaviour
{
    public Sprite ground;
    public Sprite wall;
    float tileSize;

	public Camera mainCamera;
    public PlayerController player;

	public PersistentSharpConnector conn;
	public PersistentInitGameData initData;
	
    int[][] labyrinth;

    // Use this for initialization
    void Start ()
	{
		GameObject persistentInitDataObj = GameObject.FindGameObjectWithTag("PersistentInitGameData") as GameObject;
		initData = persistentInitDataObj.GetComponent<PersistentInitGameData>();

		GameObject persistentSharpConnectorObj = GameObject.FindGameObjectWithTag("PersistentSharpConnector") as GameObject;
		conn = persistentSharpConnectorObj.GetComponent<PersistentSharpConnector>();

		if (conn != null && initData != null)
		{
			labyrinth = initData.GetLabyrinth();
		}
		else
		{
			Debug.Log("null reference PersistentSharpConnector");
			GenerateLocalLabyrinth();
		}

        tileSize = ground.textureRect.width / ground.pixelsPerUnit;
        //player.placeOnTile(1, 1);


		DrawWorld();

        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x = (labyrinth[0].Length) * tileSize / 2;
        cameraPos.y = (labyrinth.Length - 1) * tileSize / 2;

        mainCamera.transform.position = cameraPos;
        mainCamera.orthographicSize = (labyrinth.Length) * tileSize / 2;
    }

	private void GenerateLocalLabyrinth()
	{
		int nl = 25;
		int nc = 25;

		labyrinth = new int[nl][];
		for (var i = 0; i < nl; i++)
		{
			labyrinth[i] = new int[nc];
			for (var j = 0; j < nc; j++)
			{
				labyrinth[i][j] = 0;
			}
		}

		for (var i = 0; i < nl; i++)
		{
			labyrinth[i][0] = labyrinth[i][nc - 1] = 1;
		}

		for (var i = 0; i < nc; i++)
		{
			labyrinth[0][i] = labyrinth[nl - 1][i] = 1;
		}


		for (var i = 0; i < nl * 2; i++)
		{
			labyrinth[Random.Range(2, nl - 2)][Random.Range(2, nc - 2)] = 1;
		}
	}

    private void DrawWorld()
    {
        Sprite crtSprite;

        for ( var i = 0; i < labyrinth.Length; i++ )
        {
            for ( var j = 0; j < labyrinth[i].Length; j++ )
            {
                if ( labyrinth[i][j] == 1 )
                {
                    crtSprite = wall;
                }
                else
                {
                    crtSprite = ground;
                }

                GameObject newObj = new GameObject(crtSprite.name + "(" + i + "," + j + ")");
                SpriteRenderer renderer = newObj.AddComponent<SpriteRenderer>();
                renderer.sprite = crtSprite;
				renderer.sortingLayerName = "World";
                newObj.transform.position = new Vector3(j * tileSize, i * tileSize, 0);
            }
        }
    }

    public float getTileSize()
    {
        return tileSize;
    }

    public int[][] getLabyrinth()
    {
        return labyrinth;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
