using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType5 : Enemy, IMovable, ISetable
{
    public Vector2[] movePositions;
    public Vector2 pointToMove;
    public float distanceThreshold;

    private void Start()
    {
        SetEnemy();
        SetEnemyPoints();
        pointToMove = movePositions[Random.Range(0, 3)];
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
            int tempNum = Random.Range(1, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.greenNumbers[tempNum];
            result *= tempNum + 1;
        }
    }

    public void EnemyMovement()
    {
        Debug.Log(Vector2.Distance(pointToMove, transform.position));
        if (Vector2.Distance(pointToMove, transform.position) > distanceThreshold)
        {
            Vector2 position2d = transform.position;
            Vector2 direction = pointToMove - position2d;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
        else
        {
            pointToMove = movePositions[Random.Range(0, 3)];
        }
    }
}
