using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType5 : Enemy, IMovable, ISetable
{
    private void Start()
    {
        SetEnemy();
        SetEnemyPoints();
    }

    private void Update()
    {
        timeInField += Time.deltaTime;
        pointsForKill -= (int)timeInField;
        EnemyMovement();
    }

    public void SetEnemy()
    {
        result = 1;
        for (int i = 0; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is nine
            int tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.greenNumbers[tempNum];
            result *= tempNum + 1;
        }
    }

    public void EnemyMovement()
    {

    }
}
