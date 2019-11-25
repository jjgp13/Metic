using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IMovable, ISetable
{
    public int result;
    private int numberOfBalls;
    private float timeInField;
    private int pointsForKill;

    [Header("Enemy speed")]
    public float moveSpeed;

    [Header("Enemy points multiplier")]
    protected int pointsMultiplier;

    [Header("Reference to explosion particles")]
    protected GameObject explosion;

    private void Awake()
    {
        result = 0;
        numberOfBalls = transform.childCount;
        timeInField = 0;
    }

    private void Start()
    {
        SetEnemy();
        SetEnemyPoints();
    }

    protected void Update()
    {
        timeInField += Time.deltaTime;
        pointsForKill -= (int)timeInField;
        EnemyMovement();
    }

    protected void SetEnemyPoints()
    {
        pointsForKill = numberOfBalls * pointsMultiplier;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        EnemiesController._instance.enemiesInField.Remove(result);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(collision.gameObject);
        ScoreController._instance.UpdateScore(pointsForKill);
        Destroy(gameObject);
    }

    /// <summary>
    /// Only for enemy with sum
    /// </summary>
    public void SetEnemy()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is nine
            int tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.blueNumbers[tempNum];
            result += tempNum + 1;
        }
    }

    public void EnemyMovement()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
}
