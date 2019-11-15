using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{


    public static ScoreController _instance;

    public void Awake() => _instance = this;


    public int score;
    public Text scoreText;

    private void Start()
    {
        score = 0;
        scoreText.text = "0000000";
    }

    /// <summary>
    /// Update score UI when enemy is killed
    /// </summary>
    /// <param name="points">Number of point to increse</param>
    public void UpdateScore(int points)
    {
        score += points;
        scoreText.text = score.ToString("0000000");
    }
}
