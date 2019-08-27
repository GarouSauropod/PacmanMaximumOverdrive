using UnityEngine;
using System.Collections.Generic;
using DataUtility;


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
        public IntVector2 gridCoordinates;

        public Square topNeighbor;
        public Square bottomNeighbor;
        public Square leftNeighbor;
        public Square rightNeighbor;

        public int gCost;
        public int hCost;
        public Square parent;

        public int fCost
        {
            get { return gCost + hCost; }
        }
    }

    public class LevelGrid
    {
        public Square[,] squareArray;

        public void Populate(Vector3 _startingPoint, int _width, int _height, int _squareSize)
        {
            squareArray = new Square[_width, _height];

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    squareArray[i, j] = new Square();
                    squareArray[i, j].position = new Vector3(i * _squareSize + _startingPoint.x, 0 + _startingPoint.y, j * _squareSize + _startingPoint.z);
                    squareArray[i, j].gridCoordinates = new IntVector2(i, j);
                    squareArray[i, j].state = Square.State.Free;
                }
            }
        }

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
            //This can be used for a ghost that goes through walls
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

        public Path FindPathAStar(LevelGrid _grid, Square _start, Square _destination)
        {
            Path path = new Path();

            List<Square> openList = new List<Square>();
            HashSet<Square> closedSet = new HashSet<Square>();
            openList.Add(_start);

            while (openList.Count > 0)
            {
                Square currentSquare = openList[0];

                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].fCost <= currentSquare.fCost && openList[i].hCost < currentSquare.hCost)
                    {
                        currentSquare = openList[i];
                    }
                }

                openList.Remove(currentSquare);
                closedSet.Add(currentSquare);

                if (currentSquare == _destination)
                {
                    path = RetracePath(_start, _destination);
                    return path;
                }

                //Lots of copy paste here, refactor that eventually
                if (currentSquare.leftNeighbor != null && currentSquare.leftNeighbor.state == Square.State.Free && !closedSet.Contains(currentSquare.leftNeighbor))
                {
                    Square neighbor = currentSquare.leftNeighbor;
                    int newMoveCostToNeighbor = currentSquare.gCost + GetDistance(currentSquare, neighbor);
                    if (newMoveCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                    {
                        neighbor.gCost = newMoveCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, _destination);
                        neighbor.parent = currentSquare;

                        if (!openList.Contains(neighbor)) { openList.Add(neighbor); }
                    }
                }
                if (currentSquare.topNeighbor != null && currentSquare.topNeighbor.state == Square.State.Free && !closedSet.Contains(currentSquare.topNeighbor))
                {
                    Square neighbor = currentSquare.topNeighbor;
                    int newMoveCostToNeighbor = currentSquare.gCost + GetDistance(currentSquare, neighbor);
                    if (newMoveCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                    {
                        neighbor.gCost = newMoveCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, _destination);
                        neighbor.parent = currentSquare;

                        if (!openList.Contains(neighbor)) { openList.Add(neighbor); }
                    }
                }
                if (currentSquare.rightNeighbor != null && currentSquare.rightNeighbor.state == Square.State.Free && !closedSet.Contains(currentSquare.rightNeighbor))
                {
                    Square neighbor = currentSquare.rightNeighbor;
                    int newMoveCostToNeighbor = currentSquare.gCost + GetDistance(currentSquare, neighbor);
                    if (newMoveCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                    {
                        neighbor.gCost = newMoveCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, _destination);
                        neighbor.parent = currentSquare;

                        if (!openList.Contains(neighbor)) { openList.Add(neighbor); }
                    }
                }
                if (currentSquare.bottomNeighbor != null && currentSquare.bottomNeighbor.state == Square.State.Free && !closedSet.Contains(currentSquare.bottomNeighbor))
                {
                    Square neighbor = currentSquare.bottomNeighbor;
                    int newMoveCostToNeighbor = currentSquare.gCost + GetDistance(currentSquare, neighbor);
                    if (newMoveCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                    {
                        neighbor.gCost = newMoveCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, _destination);
                        neighbor.parent = currentSquare;

                        if (!openList.Contains(neighbor)) { openList.Add(neighbor); }
                    }
                }

            }

            //would return an empty path if pathing failed
            Debug.Log("pathfinding failed");
            return path;
        }

        private Path RetracePath(Square _start, Square _destination)
        {
            Path path = new Path();
            Square currentSquare = _destination;

            while (currentSquare != _start)
            {
                path.AddSquare(currentSquare);
                currentSquare = currentSquare.parent;
            }

            path.Reverse();
            return path;
        }

        private int GetDistance(Square _squareFrom, Square _squareTo)
        {
            int distanceX = Mathf.Abs(_squareFrom.gridCoordinates.x - _squareTo.gridCoordinates.x);
            int distanceY = Mathf.Abs(_squareFrom.gridCoordinates.y - _squareTo.gridCoordinates.y);
            int distance = distanceX + distanceY;
            return distance;
        }
    }

    public class Path
    {
        public List<Square> squareList = new List<Square>();

        public void AddSquare(Square square)
        {
            squareList.Add(square);
        }

        public void Reverse()
        {
            squareList.Reverse();
        }
    }

    public class LevelRepository
    {
        public string[] levels = new string[1]; //make read only

        public LevelRepository()
        {
            levels[0] = "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□;" +
                        "□0A**************□***□***************B□;" +
                        "□*□□□□*□□□□□□□□□*□*□*□*□□□□□□□□□*□□□□*□;" +
                        "□*□□□□*□□□□□□□□□*□*□*□*□□□□□□□□□*□□□□*□;" +
                        "□****************□*□*□****************□;" +
                        "□*□□□□*□□....,....,□...,....,.□□*□□□□*□;" +
                        "□******□□....,....,□...,....,.□□******□;" +
                        "□□□□□□*□□□□□□□....,□...,.□□□□□□□*□□□□□□;" +
                        "□...,□*□□....,....,1...,....,.□□*□...,□;" +
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
                        "□D***********************************C□;" +
                        "□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□";

        }
    }

}
