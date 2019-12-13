using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int result;
    protected int numberOfBalls;
    protected float timeInField;
    
    protected GameObject player;


    public enum Side
    {
        Left,
        Right
    }

    [Header("Inherited properties")]
    public float moveSpeed;
    [SerializeField]
    protected int pointsMultiplier;
    [SerializeField]
    protected int pointsForKill;
    public bool isVisible;
    public GameObject enemyExplosion;
    public TextMeshProUGUI pointsText;
    public Canvas canvas;


    protected void Awake()
    {
        isVisible = false;
        result = 0;
        numberOfBalls = transform.childCount;
        timeInField = 0;
        //Get position of the player
        player = GameObject.FindGameObjectWithTag("Player");
        //Get canvas
        canvas = FindObjectOfType<Canvas>();
    }

    protected void SetEnemyPoints()
    {
        pointsForKill = numberOfBalls * pointsMultiplier;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, 0f));
            pointsText.text = pointsForKill.ToString();
            TextMeshProUGUI inst = Instantiate(pointsText, canvas.transform, false);
            inst.rectTransform.position = screenPoint;
            Debug.Log(screenPoint);
            EnemiesController._instance.enemiesInField.Remove(result);
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            ScoreController._instance.UpdateScore(pointsForKill);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            EnemiesController._instance.enemiesInField.Remove(result);
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Get the position of the player
    /// </summary>
    /// <returns>2D vector with player's position</returns>
    protected Vector2 GetPlayerPosition()
    {
        if (player != null)
            return player.transform.position;
        else
            return Vector2.zero;
    }
}
