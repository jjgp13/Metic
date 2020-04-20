using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOverController : MonoBehaviour
{
    public static GameOverController _instance;

    public void Awake() => _instance = this;

    public bool gameOver;
    [SerializeField]
    private TextMeshProUGUI finalScoreText;
    [SerializeField]
    private Animator gameOverPanel;
    [SerializeField]
    private Animator newHighScore;


    private void Start()
    {
        gameOver = false;
        finalScoreText.text = "0000000";
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameOverAnimation()
    {
        gameOver = true;
        gameOverPanel.SetTrigger("Game Over");
    }

    public void NewHighScoreAnimation()
    {
        newHighScore.SetTrigger("New High Score");
    }

    public void StartFinalScoreCoroutine()
    {
        StartCoroutine(FinalScoreIncrease());
    }

    IEnumerator FinalScoreIncrease()
    {
        int finalScore = ScoreController._instance.score;
        int increaseScore = 0;
        while (finalScore >= 0)
        {
            if(finalScore - 50 > 0)
            {
                finalScore -= 50;
                increaseScore += 50;
            }
            else
            {
                finalScore -= 1;
                increaseScore += 1;
            }
            
            finalScoreText.text = increaseScore.ToString();
            yield return null;
        }

        if(PlayerPrefs.GetInt("High Score") < ScoreController._instance.score)
        {
            NewHighScoreAnimation();
        }
    }
}
