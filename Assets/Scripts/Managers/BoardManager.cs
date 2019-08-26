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
    [NonSerialized]
    public int width = 39;
    [NonSerialized]
    public int height = 22;
    private Vector3 gridOrigin = Vector3.zero;

    [SerializeField]
    private Transform environment;
    [SerializeField]
    private Transform dynamicObjects;

    private List<GameObject> instantiatedLevelPieces = new List<GameObject>();
    private GameObject pacman;
    private GameObject blinky;

    void Awake()
    {
        grid.Populate(new Vector3(gridOrigin.x + 1, gridOrigin.y, gridOrigin.z + 1), width, height, squareSize);
        grid.AddAllNeighbors();
    }

    public void AddPropToGrid(LevelProp _prop, IntVector2 _position)
    {
        Square referenceSquare = grid.squareArray[_position.x, _position.y];
        for (int i = 0; i < _prop.impassableSquares.Length; i++)
        {
            IntVector2 impassablePosition = _prop.impassableSquares[i];
            grid.squareArray[_position.x + impassablePosition.x, _position.y + impassablePosition.y].state = Square.State.Impassable;
        }

        GameObject element = Instantiate(_prop.physicalObject, referenceSquare.position + _prop.positionOffset, Quaternion.identity, environment);
        instantiatedLevelPieces.Add(element);
    }

    public void AddPacmanToGrid(GameObject _pacman, IntVector2 _position)
    {
        Square referenceSquare = grid.squareArray[_position.x, _position.y];
        pacman = Instantiate(_pacman, referenceSquare.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, dynamicObjects);
        pacman.GetComponent<PlayerController>().Initialize(referenceSquare);
    }

    public void AddGhostToGrid(GameObject _blinky, IntVector2 _position)
    {
        Square referenceSquare = grid.squareArray[_position.x, _position.y];
        blinky = Instantiate(_blinky, referenceSquare.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, dynamicObjects);
        blinky.GetComponent<Ghost>().Initialize(referenceSquare);
    }

    public Square GetPacmanSquare()
    {
        return pacman.GetComponent<PlayerController>().GetCurrentSquare();
    }

}
