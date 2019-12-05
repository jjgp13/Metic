using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType4 : Enemy, IMovable, ISetable
{
    [Header("Alien Type 4 properties")]
    public Side MoveTowards;

    private void Start()
    {
        SetEnemyResult();
        SetEnemyPoints();
        if(transform.position.x > 0)
            MoveTowards = Side.Left;
        else
            MoveTowards = Side.Right;
    }

    private void Update()
    {
        timeInField += Time.deltaTime;
        pointsForKill -= (int)timeInField;
        EnemyMovement();
    }

    /// <summary>
    /// Only for enemy with sum
    /// </summary>
    public void SetEnemyResult()
    {
        int tempNum = Random.Range(0, 9);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.redNumbers[tempNum];
        result -= tempNum + 1;
        for (int i = 1; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is nine
            tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.blueNumbers[tempNum];
            result += tempNum + 1;
        }
    }

    public void EnemyMovement()
    {
        if (MoveTowards == Side.Left)
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}
