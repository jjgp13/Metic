using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType1 : Enemy, IMovable, ISetable
{

    protected void Start()
    {
        SetEnemyResult();
        SetEnemyPoints();
    }

    private void Update()
    {
        if (isVisible && pointsForKill > 100)
        {
            timeInField += Time.deltaTime;
            pointsForKill -= (int)timeInField;
        }
        EnemyMovement();
    }

    /// <summary>
    /// Only for enemy with sum
    /// </summary>
    public void SetEnemyResult()
    {
        int tempNum;
        for (int i = 0; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is nine
            tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.blueNumbers[tempNum];
            result += tempNum + 1;
        }
    }

    public void EnemyMovement()
    {
        if (transform.position.y > 1)
            rb.MovePosition(rb.position + Vector2.down * moveSpeed * Time.deltaTime);        
        else
        {
            Vector2 dir = GetPlayerPosition() - (Vector2)transform.position;
            rb.MovePosition(rb.position + dir.normalized * moveSpeed * Time.deltaTime);
        }
    }

}
