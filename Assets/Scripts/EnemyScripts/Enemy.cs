using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int result = 0;
    protected int numberOfBalls;
    protected float timeInField;
    
    protected GameObject player;

    protected Rigidbody2D rb;    

    public enum EnemyLevel
    {
        easy,
        mid,
        hard
    }

    public enum Side
    {
        Left,
        Right
    }

    [Header("Inherited properties")]
    public EnemyLevel level;
    public float moveSpeed;
    [SerializeField]
    protected int pointsMultiplier;
    [SerializeField]
    protected int pointsForKill;
    public bool isVisible;
    [SerializeField]
    public GameObject enemyExplosion;
    [SerializeField]
    private TextMeshProUGUI pointsText;
    
    private Canvas canvas;


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
        //Get rb
        rb = GetComponent<Rigidbody2D>();
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

            EnemiesController._instance.enemiesInField.Remove(result);
            ScoreController._instance.UpdateScore(pointsForKill);

            Instantiate(enemyExplosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
