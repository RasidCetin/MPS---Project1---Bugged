using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController{

    
    public class Player
    {
        public Vector2 crt_dir; // (1, 0), (-1, 0), (0, 1), (0, -1)
        public Vector2 next_dir;
        public Vector2 pos;
        public Vector2 turn_point;
        public bool is_hyde;
        public float speed;
        public GameObject obj;
        public int id;
        public SpriteRenderer renderer;

        public Player(int id)
        {
            obj = new GameObject("Player" + id);
            this.id = id;
            renderer = obj.AddComponent<SpriteRenderer>();
            renderer.sprite = jekyllSprite;
            crt_dir = NONE;
            next_dir = NONE;
            speed = jekyll_speed;

        }
    }

    public int myId;

    public List<Player> players;

    public static Vector2 RIGHT, LEFT, DOWN, UP, NONE;

    const float jekyll_speed = 5.0f;
    const float hyde_speed = jekyll_speed * 2;

    public static Sprite jekyllSprite;
    public static Sprite hydeSprite;

    public WorldGeneration gen;

    const int WALL = 1;
    const int PATH = 0;

    float eps = 0.00001f;

	// Use this for initialization
	public PlayerController (WorldGeneration g) {
	    RIGHT = new Vector2(1, 0);
        LEFT = new Vector2(-1, 0);
        DOWN = new Vector2(0, -1);
        UP = new Vector2(0, 1);
        NONE = new Vector2(0, 0);

        players = new List<Player>();

        gen = g;
        /*player.crt_dir = NONE;
        player.next_dir = NONE;

        player.speed = jekyll_speed;*/

	}
	
	// Update is called once per frame
    /* De adaugat : trimite actualizari la server */
	public void treatInput () {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            players[myId].next_dir = UP;
            if (players[myId].crt_dir == DOWN)
            {
                players[myId].crt_dir = UP;
            }

            update_turning_point(players[myId]);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            players[myId].next_dir = DOWN;
            if (players[myId].crt_dir == UP)
            {
                players[myId].crt_dir = DOWN;
            }

            update_turning_point(players[myId]);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            players[myId].next_dir = LEFT;
            if (players[myId].crt_dir == RIGHT)
            {
                players[myId].crt_dir = LEFT;
            }

            update_turning_point(players[myId]);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            players[myId].next_dir = RIGHT;
            if (players[myId].crt_dir == LEFT)
            {
                players[myId].crt_dir = RIGHT;
            }

            update_turning_point(players[myId]);
        }

        for (int i = 0; i < players.Count; ++i)
        {
            if (players[i].crt_dir == NONE)
            {
                players[i].crt_dir = players[i].next_dir;
                update_turning_point(players[i]);
            }

            if (players[i].crt_dir == UP && players[i].pos.y >= players[i].turn_point.y ||
                players[i].crt_dir == DOWN && players[i].pos.y <= players[i].turn_point.y ||
                players[i].crt_dir == RIGHT && players[i].pos.x >= players[i].turn_point.x || 
                players[i].crt_dir == LEFT && players[i].pos.x <= players[i].turn_point.x)
            {
                players[i].pos = players[i].turn_point;
                players[i].crt_dir = players[i].next_dir;
                update_turning_point(players[i]);
            }

            players[i].pos = players[i].pos + Time.deltaTime * players[i].speed * players[i].crt_dir;

            players[i].obj.transform.position = players[i].pos * gen.getTileSize();
        }

	}

    int get_next_idx(float pos, float dir)
    {
        if (dir < 0)
        {
            return (int)Mathf.Floor(pos);
        }
        else if (dir > 0)
        {
            return (int)Mathf.Ceil(pos);
        }
        else return (int)pos;
    }

    void update_turning_point(Player player)
    {
        int[][] labyrinth = gen.getLabyrinth();

        int startX = get_next_idx(player.pos.x, player.crt_dir.x);
        int startY = get_next_idx(player.pos.y, player.crt_dir.y);

        if (player.crt_dir == UP)
        {
            for (int j = startY; j < labyrinth.GetLength(0); j++)
            {
				if (labyrinth [j] [startX] == WALL) {
					player.turn_point = new Vector2 (startX, j - 1);
					break;
                }
                else if (player.next_dir.x != 0)
                {
                    int tryX = startX + (int)player.next_dir.x;
                    if (labyrinth[j][tryX] == PATH)
                    {
                        player.turn_point = new Vector2(startX, j);
                        break;
                    }
                }
            }
        }
        if (player.crt_dir == DOWN)
        {
            for (int j = startY; j >= 0; j--)
            {
                if (labyrinth[j][startX] == WALL)
                {
                    player.turn_point = new Vector2(startX, j + 1);
                    break;
                }
                else if (player.next_dir.x != 0)
                {
                    int tryX = startX + (int)player.next_dir.x;
                    if (labyrinth[j][tryX] == PATH)
                    {
                        player.turn_point = new Vector2(startX, j);
                        break;
                    }
                }
            }
        }
        if (player.crt_dir == RIGHT)
        {
			int len = labyrinth[0].Length;
			for (int i = startX; i < len; i++)
            {
                if (labyrinth[startY][i] == WALL)
                {
                    player.turn_point = new Vector2(i - 1, startY);
                    break;
                }
                else if (player.next_dir.y != 0)
                {
                    int tryY = startY + (int)player.next_dir.y;
                    if (labyrinth[tryY][i] == PATH)
                    {
                        player.turn_point = new Vector2(i, startY);
                        break;
                    }
                }
            }
        }
        if (player.crt_dir == LEFT)
        {
            for (int i = startX; i >= 0; i--)
            {
                if (labyrinth[startY][i] == WALL)
                {
                    player.turn_point = new Vector2(i + 1, startY);
                    break;
                }
                else if (player.next_dir.y != 0)
                {
                    int tryY = startY + (int)player.next_dir.y;
                    if (labyrinth[tryY][i] == PATH)
                    {
                        player.turn_point = new Vector2(i, startY);
                        break;
                    }
                }
            }

        }

        if (player.crt_dir == player.next_dir)
            player.next_dir = NONE;
    }

    public void placeOnTile(Player player, Vector2 pos)
    {
        float tileSize = gen.getTileSize();
		int nl = gen.getLabyrinth().Length;

        player.pos = new Vector2(pos.x, pos.y);
        player.obj.transform.position = new Vector3(pos.x * tileSize, pos.y  * tileSize, 0);
    }

}
