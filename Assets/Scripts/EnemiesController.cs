using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    public static EnemiesController _instance;

    [Header("Spawn time")]
    public float timeToSpawnEnemy;
    public float spawnTimer;

    [Header("Enemy array")]
    public GameObject[] enemyTypes;
    public Dictionary<int, GameObject> enemiesInField;

    public void Awake() => _instance = this;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInField = new Dictionary<int, GameObject>();
        spawnTimer = timeToSpawnEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = timeToSpawnEnemy;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        //int enemyType = Random.Range(0, enemyTypes.Length);
        Instantiate(enemyTypes[0], SpawnPosition(), Quaternion.identity);
    }

    private Vector2 SpawnPosition()
    {
        float xPos = Random.Range(-2.4f, 2.4f);
        float yPos = Random.Range(4f, 4.25f);
        return new Vector2(xPos, yPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int enemyResult = collision.gameObject.GetComponent<Enemy>().result;
            if (enemiesInField.ContainsKey(enemyResult))
            {
                Destroy(collision.gameObject);
            }
            else
            {
                enemiesInField.Add(enemyResult, collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int enemyResult = collision.gameObject.GetComponent<Enemy>().result;
            enemiesInField.Remove(enemyResult);
            Destroy(collision.gameObject);
        }
    }
}
