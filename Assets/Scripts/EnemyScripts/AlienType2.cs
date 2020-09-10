using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType2 : Enemy, IMovable, ISetable
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
        for (int i = 0; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is 9
            int tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.blueNumbers[tempNum];
            result += tempNum + 1;
        }
    }

    public void EnemyMovement()
    {
        if(Vector2.Distance(GetPlayerPosition(), transform.position) > 5)
            rb.MovePosition(rb.position + Vector2.down * moveSpeed * Time.deltaTime);
        else
        {
            Vector2 dir = GetPlayerPosition() - (Vector2)transform.position;
            rb.MovePosition(rb.position + dir.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
