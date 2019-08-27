using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour
{

    private static GameLoopController gameLoopController;

    public static GameLoopController instance
    {
        get
        {
            if (!gameLoopController)
            {
                gameLoopController = FindObjectOfType(typeof(GameLoopController)) as GameLoopController;

                if (!gameLoopController)
                {
                    Debug.LogError("There needs to be one active GameLoopController script on a GameObject in your scene.");
                }
            }

            return gameLoopController;
        }
    }

    public enum GameState
    {
        StartCountdown,
        GameOn,
        GameOver,
    }

    private GameState gameState;

    private float countdownTimer = 3.5f;

    void Start()
    {
        StartCountdown();

        GameEventManager.StartListening("onGameCountdownEnded", StartGame);
        GameEventManager.StartListening("onPlayerGameOver", DoGameOver);
        
    }

    public void StartCountdown()
    {
        gameState = GameState.StartCountdown;
        GameEventManager.TriggerEvent("onGameCountdownStarted");
        Gameclock.instance.FreezeTimerTemporarily(countdownTimer);
    }

    public void StartGame(object _arg)
    {
        gameState = GameState.GameOn;
        GameEventManager.TriggerEvent("onGameStarted");
    }

    public void DoGameOver(object _arg)
    {
        gameState = GameState.GameOver;
        Gameclock.instance.StopClock();
    }

}
