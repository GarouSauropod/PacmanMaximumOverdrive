using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDMenu : MonoBehaviour
{

    [SerializeField]
    GameObject gameOverPanel;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    GameObject countdownPanel;
    [SerializeField]
    Text countdownText;

    int score;

    void Awake()
    {
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();

        GameEventManager.StartListening("onPlayerGameOver", ConjureGameOverPanel);
        GameEventManager.StartListening("onPelletEaten", UpdateScore);
        GameEventManager.StartListening("onCountdownUpdated", UpdateCountdownText);
        GameEventManager.StartListening("onGameCountdownEnded", VanishCountdownPanel);
    }

    void Update()
    {
        
    }

    private void ConjureGameOverPanel(object _arg)
    {
        gameOverPanel.SetActive(true);
    }

    private void UpdateScore(object _arg)
    {
        score += 10;
        scoreText.text = "SCORE: " + score.ToString();
    }

    private void UpdateCountdownText(object _arg)
    {
        int intCountdown = (int)Gameclock.instance.GetCountdown();
        if (intCountdown >= 1)
        {
            countdownText.text = intCountdown.ToString();
        }
        else
        {
            countdownText.text = "GO!";
        }
        
    }

    private void VanishCountdownPanel(object _arg)
    {
        countdownPanel.SetActive(false);
    }
}
