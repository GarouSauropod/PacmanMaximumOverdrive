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

    float freezeTimer = 0f;
    bool timerFrozen = false;

    //add timekeeping

    void Awake()
    {
        
    }

    void Update()
    {
        if (timerFrozen)
        {
            freezeTimer -= Time.deltaTime;
            GameEventManager.TriggerEvent("onCountdownUpdated");
            if (freezeTimer <= 0f)
            {
                UnfreezeTimer();
            }
        }
    }

    public float GetTimeFactor()
    {
        return timeFactor;
    }

    public void StopClock()
    {
        timeFactor = 0f;
    }

    public void FreezeTimerTemporarily(float _timeFrozen)
    {
        timeFactor = 0f;
        timerFrozen = true;
        freezeTimer = _timeFrozen;
    }

    public void UnfreezeTimer()
    {
        timeFactor = 1f;
        timerFrozen = false;
        freezeTimer = 0f;
        GameEventManager.TriggerEvent("onGameCountdownEnded");
    }

    public float GetCountdown()
    {
        return freezeTimer;
    }

}
