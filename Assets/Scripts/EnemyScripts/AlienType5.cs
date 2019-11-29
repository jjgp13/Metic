using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType5 : Enemy, IMovable, ISetable
{
    public Vector2 boundaryMovement;
    public Vector2 pointToMove;
    public float distanceThreshold;
    private int remainingPoints;

    private void Start()
    {
        SetEnemy();
        SetEnemyPoints();
        pointToMove = 
        remainingPoints = 3;
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
            direction = pointToMove - (Vector2)transform.position;
            MoveEnemy(direction);
        }
    }

    private void MoveEnemy(Vector2 dir)
    {
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
    }

    private Vector2 SetRandomPositionToMove()
    {
        return new Vector2(Random.Range(-boundaryMovement.x, boundaryMovement.x), Random.Range(-boundaryMovement.y, boundaryMovement.y));
    }
}
