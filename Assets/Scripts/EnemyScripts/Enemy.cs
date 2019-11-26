using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int result;
    protected int numberOfBalls;
    protected float timeInField;
    protected int pointsForKill;

    [Header("Enemy speed")]
    public float moveSpeed;

    [Header("Enemy points multiplier")]
    public int pointsMultiplier;

    [Header("Reference to explosion particles")]
    public GameObject explosion;


    protected void Awake()
    {
        result = 0;
        numberOfBalls = transform.childCount;
        timeInField = 0;
    }

    protected void SetEnemyPoints()
    {
        pointsForKill = numberOfBalls * pointsMultiplier;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            EnemiesController._instance.enemiesInField.Remove(result);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            ScoreController._instance.UpdateScore(pointsForKill);
            Destroy(gameObject);
        }
    }
}
