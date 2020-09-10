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

    public GameObject mainMenuButton;
    public GameObject tryAgainButton;

    [SerializeField]
    private TextMeshProUGUI finalScoreText;
    [SerializeField]
    private Animator gameOverPanel;
    [SerializeField]
    private GameObject newHighScore;




    private void Start()
    {
        gameOver = false;
        finalScoreText.text = "0000000";
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetTrigger("Game Over");
        NumbersController._instance.HideGameUI();
    }

    public void NewHighScoreAnimation()
    {
        newHighScore.SetActive(true);
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
            if (finalScore - 1000 > 0)
            {
                finalScore -= 1000;
                increaseScore += 1000;
            }
            else
            {
                finalScore -= 1;
                increaseScore += 1;
            }
            
            finalScoreText.text = increaseScore.ToString();
            yield return null;
        }

        CheckFinalScore();
        mainMenuButton.SetActive(true);
        tryAgainButton.SetActive(true);
    }

    public void CheckFinalScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if (ScoreController._instance.score > PlayerPrefs.GetInt("HighScore"))
            {
                NewHighScoreAnimation();
                PlayerPrefs.SetInt("HighScore", ScoreController._instance.score);
            } 
        }
        else
        {
            NewHighScoreAnimation();
            PlayerPrefs.SetInt("HighScore", ScoreController._instance.score);
        }
    }
}
