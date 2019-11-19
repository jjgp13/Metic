using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int result;
    private int numberOfBalls;
    private float timeInField;
    private int pointsForKill;

    [Header("Enemy speed")]
    public float moveSpeed;

    [Header("Reference to explosion particles")]
    public GameObject explosion;

    private void Awake()
    {
        result = 0;
        numberOfBalls = transform.childCount;
        timeInField = 0;
    }

    private void Start()
    {
        SetEnemyResult();
        SetEnemyPoints();
    }

    private void Update()
    {
        timeInField += Time.deltaTime;
        pointsForKill -= (int)timeInField * 10;
        MoveEnemy();
    }

    /// <summary>
    /// Only for enemy with sum
    /// </summary>
    private void SetEnemyResult()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is nine
            int tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.ballNumbers[tempNum];
            result += tempNum + 1;
        }
    }

    private void SetEnemyPoints()
    {
        pointsForKill = numberOfBalls * 1000;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        ScoreController._instance.UpdateScore(pointsForKill);
        Destroy(gameObject);
    }

    private void MoveEnemy()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }
}
