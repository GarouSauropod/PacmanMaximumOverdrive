using DataUtility;
using UnityEngine;
using System;
using System.Collections.Generic;
using GridUtilities;
using LevelDesignProps;

public class BoardManager : MonoBehaviour
{

    private static BoardManager boardManager;

    public static BoardManager instance
    {
        get
        {
            if (!boardManager)
            {
                boardManager = FindObjectOfType(typeof(BoardManager)) as BoardManager;

                if (!boardManager)
                {
                    Debug.LogError("There needs to be one active BoardManager script on a GameObject in your scene.");
                }
            }

            return boardManager;
        }
    }

    [NonSerialized]
    public LevelGrid grid = new LevelGrid();
    [NonSerialized]
    public int squareSize = 1;
    private Vector3 gridOrigin = Vector3.zero;

    private List<GameObject> instantiatedLevelPieces = new List<GameObject>();

    void Awake()
    {
        PopulateGrid(new Vector3(gridOrigin.x + 1, gridOrigin.y, gridOrigin.z + 1), 39, 22);
        grid.AddAllNeighbors();
    }

    public void PopulateGrid(Vector3 _startingPoint, int _width, int _height)
    {
        grid.squareArray = new Square[_width, _height];

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                grid.squareArray[i, j] = new Square();
                grid.squareArray[i, j].position = new Vector3(i*squareSize + _startingPoint.x, 0 + _startingPoint.y, j*squareSize + _startingPoint.z);
                grid.squareArray[i, j].state = Square.State.Free;
            }
        }
    }

    public void AddPropToGrid(LevelProp _prop, IntVector2 _position)
    {
        Square referenceSquare = grid.squareArray[_position.x, _position.y];
        for (int i = 0; i < _prop.impassableSquares.Length; i++)
        {
            IntVector2 impassablePosition = _prop.impassableSquares[i];
            grid.squareArray[_position.x + impassablePosition.x, _position.y + impassablePosition.y].state = Square.State.Impassable;
        }

        GameObject element = Instantiate(_prop.physicalObject, referenceSquare.position + _prop.positionOffset, Quaternion.identity);
        instantiatedLevelPieces.Add(element);
    }

}
