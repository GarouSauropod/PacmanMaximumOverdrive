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
                    break;
                default:
                    break;
            }
        }
    }

}
