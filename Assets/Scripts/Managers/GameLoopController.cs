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
        Victory,
    }

    private GameState gameState;

    private float countdownTimer = 3.5f;
    private int totalNumberOfPellets = 0;
    private int pelletsBeforePenOpens = 20;
    private int pelletEatenCount = 0;
    private bool ghostPenOpen = false;

    void Start()
    {
        StartCountdown();

        GameEventManager.StartListening("onGameCountdownEnded", StartGame);
        GameEventManager.StartListening("onPlayerGameOver", DoGameOver);
        GameEventManager.StartListening("onPlayerWins", DoVictory);
        GameEventManager.StartListening("onPelletEaten", IncrementPelletEatenCount);

    }

    public void IncrementTotalPelletCount()
    {
        totalNumberOfPellets += 1;
    }

    private void IncrementPelletEatenCount(object _arg)
    {
        pelletEatenCount += 1;
        if (pelletEatenCount >= pelletsBeforePenOpens && !ghostPenOpen)
        {
            GameEventManager.TriggerEvent("onGhostPenOpen");
            ghostPenOpen = true;
        }
        if (pelletEatenCount == totalNumberOfPellets)
        {
            GameEventManager.TriggerEvent("onPlayerWins");
        }
    }

    private void StartCountdown()
    {
        gameState = GameState.StartCountdown;
        GameEventManager.TriggerEvent("onGameCountdownStarted");
        Gameclock.instance.FreezeTimerTemporarily(countdownTimer);
    }

    private void StartGame(object _arg)
    {
        gameState = GameState.GameOn;
        GameEventManager.TriggerEvent("onGameStarted");
    }

    private void DoGameOver(object _arg)
    {
        gameState = GameState.GameOver;
        Gameclock.instance.StopClock();
    }

    private void DoVictory(object _arg)
    {
        gameState = GameState.Victory;
        Gameclock.instance.StopClock();
    }

}
