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

    //Add travel nodes for the PathFinder

    public class PathFinder
    {
        private static PathFinder instance = null;

        public static PathFinder Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PathFinder();
                }
                return instance;
            }
        }

        public Path FindPath(Square _start, Square _destination)
        {
            //I call this the lazy* algorithm cause I'm too lazy to implement the actual A*
            Path path = new Path();
            Square currentSquare = _start;

            int deltaX = (int)(_destination.position.x - currentSquare.position.x);
            int deltaZ = (int)(_destination.position.z - currentSquare.position.z);

            while (Mathf.Abs(deltaX) > 0 || Mathf.Abs(deltaZ) > 0)
            {
                if (Mathf.Abs(deltaX) >= Mathf.Abs(deltaZ))
                {
                    if (deltaX > 0)
                    {
                        path.AddSquare(currentSquare.rightNeighbor);
                        currentSquare = currentSquare.rightNeighbor;
                    }
                    else
                    {
                        path.AddSquare(currentSquare.leftNeighbor);
                        currentSquare = currentSquare.leftNeighbor;
                    }
                }
                else
                {
                    if (deltaZ > 0)
                    {
                        path.AddSquare(currentSquare.topNeighbor);
                        currentSquare = currentSquare.topNeighbor;
                    }
                    else
                    {
                        path.AddSquare(currentSquare.bottomNeighbor);
                        currentSquare = currentSquare.bottomNeighbor;
                    }
                }

                deltaX = (int)(_destination.position.x - currentSquare.position.x);
                deltaZ = (int)(_destination.position.z - currentSquare.position.z);
            }

            return path;
        }

        public Path FindPathAStar()
        {
            Path path = new Path();

            //Do A* here

            return path;
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
                        "□****************□***□****************□;" +
                        "□*□□□□*□□□□□□□□□*□*□*□*□□□□□□□□□*□□□□*□;" +
                        "□*□□□□*□□□□□□□□□*□*□*□*□□□□□□□□□*□□□□*□;" +
                        "□****************□*□*□****************□;" +
                        "□*□□□□*□□....,....,□...,....,.□□*□□□□*□;" +
                        "□******□□....,....,□...,....,.□□******□;" +
                        "□□□□□□*□□□□□□□....,□...,.□□□□□□□*□□□□□□;" +
                        "□...,□*□□0...,....,....,....,1□□*□...,□;" +
                        "□□□□□□*□□.□.□□□□□.,...□□□□□.□.□□*□□□□□□;" +
                        "□...,.*.,.□.□,....,....,..□.□...*,...,□;" +
                        "□□□□□□*□□.□.□□□□□□□□□□□□□□□.□.□□*□□□□□□;" +
                        "□...,□*□□....,....,....,....,.□□*□...,□;" +
                        "□□□□□□*□□.□□□□□□□□□□□□□□□□□□□.□□*□□□□□□;" +
                        "□******************□******************□;" +
                        "□*□□□□*□□□□□□□□□□□*□*□□□□□□□□□□□*□□□□*□;" +
                        "□****□***************************□****□;" +
                        "□□□□*□*□□□□□□□□□□□□□□□□□□□□□□□□□*□*□□□□;" +
                        "□******************□******************□;" +
                        "□*□□□□□□□□*□□□□□□□*□*□□□□□□□*□□□□□□□□*□;" +
                        "□*************************************□;" +
                        "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□";

        }
    }

}
