using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyResult : Enemy
{
    public int numberOfBalls;
    public int enemyResult;

    // Start is called before the first frame update
    void Start()
    {
        numberOfBalls = transform.childCount;
        enemyResult = 0;
        SetNumbers();
    }

    private void Update()
    {
        if(NumbersController._instance.result == enemyResult)
        {
            Destroy(gameObject);
        }
    }

    private void SetNumbers()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            int number = Random.Range(1, 10);
            enemyResult += number;
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = ballNumbers[number - 1];
        }
    }
}
