using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace TCPServer
{
	class GameServer : Server
	{
		MazeGenerator mg;
		int numOfReplies;
        const int WALL = 0;
        const int PATH = 1;

        public GameServer() : base()
		{
		}

		// Called when the game starts
		public void StartGame()
		{
			Console.WriteLine("Game Started!");

			// Deny any atempt to connect at the server
			_acceptClients = false;

			mg = new MazeGenerator();
			int[,] maze = mg.computeFinalMap();

			SendToAllClients("M2" + CreateMazePayload(maze));
			numOfReplies = 0;
		}

        public override void ProcessPayload(ClientNode c, string payload)
        {
            Console.WriteLine(c.ToString() + " " + payload);

            if (payload == "Disconnect")
            {
                DisconnectClient(c);
                c.Disconnect();
            }

            if (payload == "Done creating labyrinth")
            {
                lock(mg)
                {
                    numOfReplies++;
                    Console.WriteLine("numOfReplies = : " + numOfReplies);
                }
                if (numOfReplies == _connectedClients.Count)
                {
                    Console.WriteLine("Toti au primit matricea");
                    numOfReplies = 0;
                    /* numarul de zone din harta */
                    int zones = 2;
                    int linesPerZone = mg.getDimension() / 2;
                    int columnsPerZone = 0, zonesPerLine = 0;
                    int startColumnZone = 0, startLineZone = 0;
                    int clientRow = 0, clientColumn = 0;
                    Random random = new Random();
                    string message = "M3";
                    while (zones < _connectedClients.Count)
                    {
                        zones *= 2;
                    }

                    columnsPerZone = (mg.getDimension() * 2 - 1) / (zones / 2);

                    zonesPerLine = zones / 2;

                    for (int i = 0; i < _connectedClients.Count; ++i)
                    {
                        startColumnZone = (i % zonesPerLine) * columnsPerZone;
                        startLineZone = (i / zonesPerLine) * linesPerZone;
                        do
                        {
                            clientRow = random.Next(startLineZone, startLineZone + linesPerZone);
                            clientColumn = random.Next(startColumnZone, startColumnZone + columnsPerZone);
                        } while (mg.finalMap[clientRow, clientColumn] == WALL);

                        _connectedClients[i].player = new ClientNode.Player(i);


                        _connectedClients[i].player.pos = new Vector2(clientColumn, clientRow);
                        message += _connectedClients[i].player.pos.X + "," + _connectedClients[i].player.pos.Y + "|";

                    }


                    // Send message to all clients to start the game
                    SendToAllClients(message);
                    Console.WriteLine("Am trimis mesajul :  " + message);
                }
            }

            if (payload == "Done positioning")
            {
                lock(mg)
                {
                    numOfReplies++;
                }
                if (numOfReplies == _connectedClients.Count)
                {
                    Console.WriteLine("Trimit start");
                    SendToAllClients("Start");
                }
            }
        }

		private string CreateMazePayload(int[,] maze)
		{
			string result = "";
			int dim = mg.getDimension();

			result += dim + "|" + (dim * 2 - 1) + "|";
			
			for (int i = 0; i < dim; ++i)
			{
				for (int j = 0; j < 2 * dim - 1; ++j)
					if (maze[i, j] == 0)
						result += "1";
					else
						result += "0";
			}

			return result;
		}
	}
}
