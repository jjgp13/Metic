using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int result;
    protected int numberOfBalls;
    protected float timeInField;
    protected int pointsForKill;
    protected Transform playerPosition;

    public enum Side
    {
        Left,
        Right
    }

    [Header("Inherited properties")]
    public float moveSpeed;
    public int pointsMultiplier;
    public GameObject enemyExplosion;


    protected void Awake()
    {
        result = 0;
        numberOfBalls = transform.childCount;
        timeInField = 0;
        //Get position of the player
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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


    protected Vector2 GetPlayerPosition()
    {
        return playerPosition.position;
    }
}
