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
    private int counter;

    private void Start()
    {
        score = 0;
        scoreText.text = "0000000";
        counter = 100000;
    }

    private void Update()
    {
        if (counter < 0)
        {
            scoreText.GetComponent<Animator>().SetTrigger("goal");
            counter = 100000;
        }
    }

    /// <summary>
    /// Update score UI when enemy is killed
    /// </summary>
    /// <param name="points">Number of point to increse</param>
    public void UpdateScore(int points)
    {
        StartCoroutine(IncreaseScore(points));
    }

    IEnumerator IncreaseScore(int points)
    {
        while (points >= 0)
        {
            if (points - 50 > 0)
            {
                points -= 50;
                score += 50;
                counter -= 50;
            }
            else
            {
                points--;
                score++;
                counter--;
            }
            scoreText.text = score.ToString("0000000");
            
            yield return null;
        }
    }
}
