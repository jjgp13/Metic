using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType5 : Enemy, IMovable, ISetable
{
    [Header("Alien Type 5 properties")]
    public Vector2 xMovLimit;
    public Vector2 yMovLimit;
    private Vector2 pointToMove;
    public float distanceThreshold;
    private int remainingPoints;

    private void Start()
    {
        SetEnemyResult();
        SetEnemyPoints();
        pointToMove = SetRandomPositionToMove();
        remainingPoints = 3;
    }

    private void Update()
    {
        timeInField += Time.deltaTime;
        pointsForKill -= (int)timeInField;
        EnemyMovement();
    }

    public void SetEnemyResult()
    {
        result = 1;
        for (int i = 0; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is 9
            int tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.greenNumbers[tempNum];
            //Add one unit to result
            result *= tempNum + 1;
        }
    }

    public void EnemyMovement()
    {
        Vector2 direction;
        if(remainingPoints > 0)
        {
            if (Vector2.Distance(pointToMove, transform.position) > distanceThreshold)
            {
                direction = pointToMove - (Vector2)transform.position;
                MoveEnemy(direction);
            }
            else
            {
                pointToMove = SetRandomPositionToMove();
                remainingPoints--;
            }
        }
        else
        {
            direction = GetPlayerPosition() - (Vector2)transform.position;
            MoveEnemy(direction);
        }
    }

    private void MoveEnemy(Vector2 dir)
    {
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
    }

    private Vector2 SetRandomPositionToMove()
    {
        return new Vector2(Random.Range(xMovLimit.x, xMovLimit.y), Random.Range(yMovLimit.x, yMovLimit.y));
    }
}
