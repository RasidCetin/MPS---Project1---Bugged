using UnityEngine;
using System.Collections;

public class PersistentInitGameData : MonoBehaviour
{
	public int[][] labyrinth;

	bool doneCreatingLabyrinth;

	void Awake()
	{
		// make this object persistent
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		doneCreatingLabyrinth = false;
	}
	
	public void CreateLabyrinth(string s)
	{
		Debug.Log(s);

		string[] res = s.Split('|');

		int nl = int.Parse(res[0]);
		int nc = int.Parse(res[1]);

		labyrinth = new int[nl][];
		int k = 0;
		for (var i = 0; i < nl; i++)
		{
			labyrinth[i] = new int[nc];
			for (var j = 0; j < nc; j++)
			{
				labyrinth[i][j] = res[2][k++] - '0';
			}
		}

		doneCreatingLabyrinth = true;
	}

	public bool DoneCreatingLabyrinth()
	{
		return doneCreatingLabyrinth;
	}

	public int[][] GetLabyrinth()
	{
		return labyrinth;
	}
}
