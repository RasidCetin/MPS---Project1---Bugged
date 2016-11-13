using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class MazeGenerator
{
    int[,] map;
    public int[,] finalMap;
    int dimension;
    int sRow = 0, sCol = 0;
    Random random = new Random();

    public MazeGenerator(int dimension = 25)
    {
        this.dimension = dimension;
    }

    public void showMap()
    {
        for (int i = 0; i < dimension; ++i)
        {
            for (int j = 0; j < dimension; ++j)
                if (map[i, j] == 0)
                    Console.Write("#" + " ");
                else
                    Console.Write("." + " ");
            Console.WriteLine();
        }
    }
    public void showFinalMap()
    {
        for (int i = 0; i < dimension; ++i)
        {
            for (int j = 0; j < 2 * dimension - 1; ++j)
                if (finalMap[i, j] == 0)
                    Console.Write("#" + " ");
                else
                    Console.Write("." + " ");
            Console.WriteLine();
        }
    }

    public void initializeMap()
    {
        for (int i = 1; i < dimension - 1; ++i)
            map[1, i] = map[dimension - 1, i] = map[i, 1] = 1;

        map[dimension - 1, 1] = 1;

        bool notFound = true;
        while (notFound)
        {
            sRow = random.Next(0, dimension - 2);
            sCol = random.Next(0, dimension - 1);

            if (isGood(sRow, sCol) == true)
                break;
        }
        map[sRow, sCol] = 1;
    }

    public int[] generateDirections()
    {
        int[] directions = new int[4];
        for (int i = 0; i < 4; ++i)
            directions[i] = i + 1;

        int[] randomDirections = directions.OrderBy(x => random.Next()).ToArray();
        return randomDirections;
    }

    public bool isGood(int line, int col)
    {
        if (line > 1 && col > 1 && line % 2 == 1 && col % 2 == 1)
            return true;
        return false;
    }
    public void generateMaze(int row, int col)
    {
        int[] dirs = generateDirections();

        for (int i = 0; i < dirs.Length; ++i)
        {
            switch (dirs[i])
            {
                case 1: // UP
                    {
                        if (row - 2 <= 0)
                            continue;
                        if (map[row - 2, col] != 1)
                        {
                            map[row - 1, col] = 1;
                            map[row - 2, col] = 1;
                            generateMaze(row - 2, col);
                        }
                        break;
                    }
                case 2: // Right
                    if (col + 2 >= dimension - 1)
                        continue;
                    if (map[row, col + 2] != 1)
                    {
                        map[row, col + 1] = 1;
                        map[row, col + 2] = 1;
                        generateMaze(row, col + 2);
                    }
                    break;
                case 3: //Down
                    if (row + 2 >= dimension - 1)
                        continue;
                    if (map[row + 2, col] != 1)
                    {
                        map[row + 1, col] = 1;
                        map[row + 2, col] = 1;
                        generateMaze(row + 2, col);
                    }
                    break;
                case 4: // Left
                    if (col - 2 <= 0)
                        continue;
                    if (map[row, col - 2] != 1)
                    {
                        map[row, col - 1] = 1;
                        map[row, col - 2] = 1;
                        generateMaze(row, col - 2);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public bool isDeadEnd(int row, int col)
    {
        int nr = 0;
        if (map[row + 1, col] == 0)
            ++nr;
        if (map[row - 1, col] == 0)
            ++nr;
        if (map[row, col + 1] == 0)
            ++nr;
        if (map[row, col - 1] == 0)
            ++nr;
        return nr == 3;
    }
    public void cleanCell(int row, int col)
    {
        if (row - 1 > 0 && map[row - 1, col] == 0)
            map[row - 1, col] = 1;
        else if (col - 1 > 0 && map[row, col - 1] == 0)
            map[row, col - 1] = 1;
        else if (row + 1 < dimension - 1 && map[row + 1, col] == 0)
            map[row + 1, col] = 1;
        else if (col + 1 < dimension - 1 && map[row, col + 1] == 0)
            map[row, col + 1] = 1;
    }
    public void cleanMaze()
    {
        for (int i = 1; i < dimension - 1; ++i)
        {
            for (int j = 1; j < dimension - 1; ++j)
                if (map[i, j] == 1 && isDeadEnd(i, j))
                    cleanCell(i, j);

        }

    }

    public int[,] rotate90()
    {
        int[,] matrix = new int[dimension, dimension];

        for (int i = 0; i < dimension; ++i)
        {
            for (int j = 0; j < dimension; ++j)
            {
                matrix[j, i] = map[i, dimension - j - 1];
            }
        }
        return matrix;
    }

    public void getFinalMap()
    {
        for (int i = 0; i < dimension; ++i)
        {
            for (int j = 0; j < dimension; ++j)
            {
                finalMap[i, j] = map[i, j];
                if (dimension - 2 - j > 0)
                    finalMap[i, dimension + j] = map[i, dimension - 2 - j];
            }
        }

    }

    public int[,] computeFinalMap()
    {
        map = new int[dimension, dimension];
        finalMap = new int[dimension, 2 * dimension - 1];
        initializeMap();
        generateMaze(sRow, sCol);
        cleanMaze();
        map = rotate90();
        getFinalMap();
        return finalMap;
    }

	public int getDimension()
	{
		return dimension;
	}
}
