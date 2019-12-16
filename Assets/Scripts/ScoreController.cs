using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{

    public static ScoreController _instance;

    public void Awake() => _instance = this;

    public int score;
    public TextMeshProUGUI scoreText;

    public bool enemyDestroy;

    private void Start()
    {
        score = 0;
        scoreText.text = "0000000";
        enemyDestroy = false;
        StartCoroutine(UpdateScore(10000));
    }

    private void Update()
    {
        if (enemyDestroy)
        {
            UpdateScore(1000);
            enemyDestroy = false;
        }
    }

    /// <summary>
    /// Update score UI when enemy is killed
    /// </summary>
    /// <param name="points">Number of point to increse</param>
    IEnumerator UpdateScore(int points)
    {
        for (int i = 1; i <= points; i++)
        {
            score+=10;
            scoreText.text = score.ToString("0000000");
            yield return null;
        }
    }
}
