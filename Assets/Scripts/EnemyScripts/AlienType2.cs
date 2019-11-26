using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType2 : Enemy, IMovable, ISetable
{

    protected void Start()
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
