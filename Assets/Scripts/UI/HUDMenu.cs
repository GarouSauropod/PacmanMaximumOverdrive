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

    int score;

    void Awake()
    {
        score = 0;
        scoreText.text = "SCORE: " + score.ToString();

        GameEventManager.StartListening("onPlayerGameOver", ConjureGameOverPanel);
        GameEventManager.StartListening("onPelletEaten", UpdateScore);
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
}
