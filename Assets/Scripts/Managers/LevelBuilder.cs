using DataUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridUtilities;
using LevelDesignProps;

public class LevelBuilder : MonoBehaviour
{

    private static LevelBuilder levelBuilder;

    public static LevelBuilder instance
    {
        get
        {
            if (!levelBuilder)
            {
                levelBuilder = FindObjectOfType(typeof(LevelBuilder)) as LevelBuilder;

                if (!levelBuilder)
                {
                    Debug.LogError("There needs to be one active LevelBuilder script on a GameObject in your scene.");
                }
            }

            return levelBuilder;
        }
    }

    [SerializeField]
    BoardManager boardManager;
    [SerializeField]
    LevelProp barrier;
    [SerializeField]
    LevelProp foodPellet;

    [SerializeField]
    GameObject pacmanTemplate;
    [SerializeField]
    GameObject blinkyTemplate;
    [SerializeField]
    GameObject pinkyTemplate;
    [SerializeField]
    GameObject inkyTemplate;
    [SerializeField]
    GameObject clydeTemplate;

    private LevelRepository levelRepository = new LevelRepository();

    void Start()
    {
        InitializeLevelLayout();
    }

    public void InitializeLevelLayout()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        string blueprint = levelRepository.levels[0]; //Make this accept any level as argument
        int x = 0;
        int y = 0;

        for (int i = 0; i < blueprint.Length; i++)
        {
            switch(blueprint[i])
            {
                case '.': x += 1;
                    break;
                case ',': x += 1;
                    break;
                case ';': x = 0; y += 1;
                    break;
                case '□': boardManager.AddPropToGrid(barrier, new IntVector2(x, y)); x += 1;
                    break;
                case '*': boardManager.AddPropToGrid(foodPellet, new IntVector2(x, y)); x += 1;
                    GameLoopController.instance.IncrementTotalPelletCount();
                    break;
                case '0': boardManager.AddPacmanToGrid(pacmanTemplate, new IntVector2(x, y)); x += 1;
                    break;
                case '1': boardManager.AddGhostToGrid(1, blinkyTemplate, new IntVector2(x, y)); x += 1;
                    break;
                case '2': boardManager.AddGhostToGrid(2, pinkyTemplate, new IntVector2(x, y)); x += 1;
                    break;
                case '3': boardManager.AddGhostToGrid(3, inkyTemplate, new IntVector2(x, y)); x += 1;
                    break;
                case '4': boardManager.AddGhostToGrid(4, clydeTemplate, new IntVector2(x, y)); x += 1;
                    break;
                case 'A': boardManager.AddGhostCornerToGrid(1, new IntVector2(x, y));
                    boardManager.AddPropToGrid(foodPellet, new IntVector2(x, y)); x += 1;
                    GameLoopController.instance.IncrementTotalPelletCount();
                    break;
                case 'B': boardManager.AddGhostCornerToGrid(2, new IntVector2(x, y));
                    boardManager.AddPropToGrid(foodPellet, new IntVector2(x, y)); x += 1;
                    GameLoopController.instance.IncrementTotalPelletCount();
                    break;
                case 'C': boardManager.AddGhostCornerToGrid(3, new IntVector2(x, y));
                    boardManager.AddPropToGrid(foodPellet, new IntVector2(x, y)); x += 1;
                    GameLoopController.instance.IncrementTotalPelletCount();
                    break;
                case 'D': boardManager.AddGhostCornerToGrid(4, new IntVector2(x, y));
                    boardManager.AddPropToGrid(foodPellet, new IntVector2(x, y)); x += 1;
                    GameLoopController.instance.IncrementTotalPelletCount();
                    break;
                default:
                    break;
            }
        }
    }

}
