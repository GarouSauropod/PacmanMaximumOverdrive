using UnityEngine;
using System.Collections.Generic;


namespace GridUtilities
{

    public class Square
    {
        public enum State
        {
            Impassable,
            Free
        };
        public State state;

        public Vector3 position;
        public Square topNeighbor;
        public Square bottomNeighbor;
        public Square leftNeighbor;
        public Square rightNeighbor;
    }

    public class LevelGrid
    {
        public Square[,] squareArray;

        public void AddAllNeighbors()
        {
            for (int x = 0; x < squareArray.GetLength(0); x++)
            {
                for (int z = 0; z < squareArray.GetLength(1); z++)
                {
                    if (x != 0)
                    {
                        squareArray[x, z].leftNeighbor = squareArray[x - 1, z];
                    }
                    if (z != 0)
                    {
                        squareArray[x, z].bottomNeighbor = squareArray[x, z - 1];
                    }
                    if (x != squareArray.GetLength(0) - 1)
                    {
                        squareArray[x, z].rightNeighbor = squareArray[x + 1, z];
                    }
                    if (z != squareArray.GetLength(1) - 1)
                    {
                        squareArray[x, z].topNeighbor = squareArray[x, z + 1];
                    }
                }
            }
        }
    }

    public class Path
    {
        public List<Square> squareList = new List<Square>();

        public void AddSquare(Square square)
        {
            squareList.Add(square);
        }
    }

    public class LevelRepository
    {
        public string[] levels = new string[1]; //make read only

        public LevelRepository()
        {
            levels[0] = "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□;" +
                        "□...,...,....,...□,..□.,....,....,...,□;" +
                        "□.□□□□.□□□□□□□□□.□,□.□.□□□□□□□□□.□□□□,□;" +
                        "□.□□□□.□□□□□□□□□.□,□.□.□□□□□□□□□.□□□□,□;" +
                        "□...,...,....,...□,□.□.,....,....,...,□;" +
                        "□.□□□□.□□....,....,□...,....,.□□.,...,□;" +
                        "□...,..□□....,....,□...,....,.□□.,...,□;" +
                        "□□□□□□.□□□□□□□....,□...,.□□□□□□□.□□□□□□;" +
                        "□...,□.□□....,....,....,....,.□□.□...,□;" +
                        "□□□□□□.□□.□.□□□□□.,...□□□□□.□.□□.□□□□□□;" +
                        "□...,...,.□.□,....,....,..□.□....,...,□;" +
                        "□□□□□□.□□.□.□□□□□□□□□□□□□□□.□.□□.□□□□□□;" +
                        "□...,□.□□....,....,....,....,.□□.□...,□;" +
                        "□□□□□□.□□.□□□□□□□□□□□□□□□□□□□.□□.□□□□□□;" +
                        "□...,...,....,.....□...,....,....,...,□;" +
                        "□.□□□□.□□□□□□□□□□□.□.□□□□□□□□□□□.□□□□,□;" +
                        "□...,□..,....,....,....,....,....□...,□;" +
                        "□□□□,□.□□□□□□□□□□□□□□□□□□□□□□□□□.□.□□□□;" +
                        "□...,...,....,.....□...,....,....,...,□;" +
                        "□.□.□□□□□□□□□□□□□□.□.□□□□□□□□□□□□□□.□,□;" +
                        "□...,...,....,....,....,....,....,...,□;" +
                        "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□";

        }
    }

}
