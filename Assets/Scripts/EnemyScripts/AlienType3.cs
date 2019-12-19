using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType3 : Enemy, IMovable, ISetable
{
    private GameObject[] balls;
    [Header("Alien type 5 properties")]
    public float rotationSpeed;

    private void Start()
    {
        balls = new GameObject[3];
        for (int i = 0; i < transform.childCount; i++)
        {
            balls[i] = transform.GetChild(i).gameObject;
        }
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
        MoveBallsAround();
    }

    public void SetEnemyResult()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            //From 1 to 9 -> 0 is 1 and 8 is 9
            int tempNum = Random.Range(0, 9);
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.blueNumbers[tempNum];
            //Add one unit to result
            result += tempNum + 1;
        }
    }

    public void EnemyMovement()
    {
        Vector2 direction = GetPlayerPosition() - (Vector2)transform.position;
        MoveEnemy(direction);
    }

    private void MoveEnemy(Vector2 dir)
    {
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
    }

    private void MoveBallsAround()
    {
        foreach (var item in balls)
        {
            item.transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
            item.transform.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
