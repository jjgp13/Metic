﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;


public class EnemiesController : MonoBehaviour
{

    public static EnemiesController _instance;

    [SerializeField]
    private float spawnEnemyXLimit;
    [SerializeField]
    private float spawnEnemyYLimit;

    [Header("Spawn times")]
    public float easyLevelEnemyTime;
    public float midLevelEnemyTime;
    public float hardLevelEnemyTime;
    [SerializeField]
    private float easyLevelSpawnTimer;
    [SerializeField]
    private float midLevelSpawnTimer;
    [SerializeField]
    private float hardLevelSpawnTimer;

    /// <summary>
    /// This will help to no overlap enemies on the screen, there should be a minumum distance between spawn positions.
    /// </summary>
    private Vector2 lastSpawnEnemyPosition;


    [Header("Enemy array")]
    public GameObject[] easyLevelEnemies;
    public GameObject[] midLevelEnemies;
    public GameObject[] hardLevelEnemies;
    public Dictionary<int, GameObject> enemiesInField;


    public bool hasGameStart;

    public void Awake() => _instance = this;


    // Start is called before the first frame update
    void Start()
    {
        enemiesInField = new Dictionary<int, GameObject>();
        easyLevelSpawnTimer = 1f;
        midLevelSpawnTimer = midLevelEnemyTime;
        hardLevelSpawnTimer = hardLevelEnemyTime;
        lastSpawnEnemyPosition = Vector2.zero;
        hasGameStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemiesInField.Count == 0)
        //    SpawnEnemy(easyLevelEnemies[RandomEnemy(easyLevelEnemies.Length)]);
        if (hasGameStart)
            TimerEnemySpawn();
    }

    private void TimerEnemySpawn()
    {
        if (!GameOverController._instance.gameOver)
        {
            easyLevelSpawnTimer -= Time.deltaTime;
            if (easyLevelSpawnTimer <= 0)
            {
                easyLevelSpawnTimer = easyLevelEnemyTime;
                SpawnEnemy(easyLevelEnemies[RandomEnemy(easyLevelEnemies.Length)]);
            }

            //midLevelSpawnTimer -= Time.deltaTime;
            //if (midLevelSpawnTimer <= 0)
            //{
            //    midLevelSpawnTimer = midLevelEnemyTime;
            //    SpawnEnemy(midLevelEnemies[RandomEnemy(midLevelEnemies.Length)]);
            //}

            //hardLevelSpawnTimer -= Time.deltaTime;
            //if (hardLevelSpawnTimer <= 0)
            //{
            //    hardLevelSpawnTimer = hardLevelEnemyTime;
            //    SpawnEnemy(hardLevelEnemies[RandomEnemy(hardLevelEnemies.Length)]);
            //}
        }

    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, SpawnPosition(), Quaternion.identity);
    }

    private int RandomEnemy(int enemyCount)
    {
        return Random.Range(0, enemyCount);
    }

    private Vector2 SpawnPosition()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnEnemyXLimit, spawnEnemyXLimit), spawnEnemyYLimit);
        while(Mathf.Abs(lastSpawnEnemyPosition.x - spawnPosition.x) < 1)
            spawnPosition = new Vector2(Random.Range(-spawnEnemyXLimit, spawnEnemyXLimit), spawnEnemyYLimit);
        lastSpawnEnemyPosition = spawnPosition;
        return spawnPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject enemy = collision.gameObject;
        if (enemy.CompareTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().isVisible = true;
            int enemyResult = collision.gameObject.GetComponent<Enemy>().result;
            if (enemiesInField.ContainsKey(enemyResult))
            {
                if(enemy.GetComponent<Enemy>().level == Enemy.EnemyLevel.easy)
                    SpawnEnemy(easyLevelEnemies[RandomEnemy(easyLevelEnemies.Length)]);
                else if (enemy.GetComponent<Enemy>().level == Enemy.EnemyLevel.mid)
                    SpawnEnemy(midLevelEnemies[RandomEnemy(midLevelEnemies.Length)]);
                else
                    SpawnEnemy(hardLevelEnemies[RandomEnemy(hardLevelEnemies.Length)]);
                Destroy(enemy);
            }
            else
                enemiesInField.Add(enemyResult, collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    public void PrintKeys()
    {
        List<int> keys = new List<int>(enemiesInField.Keys);
        Debug.Log("Enemies in Field");
        foreach (var key in keys)
        {
            Debug.Log(key);
        }
    }
}
