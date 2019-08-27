using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameclock : MonoBehaviour
{
    private static Gameclock gameclock;

    public static Gameclock instance
    {
        get
        {
            if (!gameclock)
            {
                gameclock = FindObjectOfType(typeof(Gameclock)) as Gameclock;

                if (!gameclock)
                {
                    Debug.LogError("There needs to be one active Gameclock script on a GameObject in your scene.");
                }
            }

            return gameclock;
        }
    }

    [SerializeField]
    float timeFactor = 1f;

    //add timekeeping

    void Start()
    {
        GameEventManager.StartListening("onPlayerGameOver", StopClock);
    }

    void Update()
    {
        
    }

    public float GetTimeFactor()
    {
        return timeFactor;
    }

    private void StopClock(object _arg) //Change this so it's called by the gameLoopController
    {
        timeFactor = 0f;
    }
}
