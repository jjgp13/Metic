using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienType6 : Enemy, IMovable, ISetable
{
    
    public Queue<Vector2> movePoints = new Queue<Vector2>();
    [Header("Alien Type 6 properties")]
    [SerializeField]
    private float distanceToPoint;
    [SerializeField]
    private Vector2 movePoint;
    public float rotationSpeed;
    
    void Start()
    {
        SetEnemyResult();
        SetPointsToMove(Random.Range(5, 10));

        distanceToPoint = 0f;
        movePoint = Vector2.zero;
    }


    void FixedUpdate()
    {
        EnemyMovement();
    }

    public void SetEnemyResult()
    {
        //This will be given the game difficulty.
        numberOfBalls = Random.Range(2, 11);
        //Set balls values
        SetBallsRandom();

        //Deactive remain balls
        for (int i = numberOfBalls; i < 10; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }

    public void EnemyMovement()
    {
        transform.Translate(0, Vector2.down.y * moveSpeed * Time.deltaTime, 0f);
        //Vector2 dir = 
        //GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + Vector2.down * moveSpeed * Time.deltaTime);
        //If there are points in the Queue, select one and go to it.
        //This will give the player change to make the calculus to destroy the enemy
        if (movePoints.Count > 0)
        {
            if (distanceToPoint < 0.1f)
                movePoint = movePoints.Dequeue();

            MoveToPoint(movePoint);
            distanceToPoint = Vector2.Distance(transform.position, movePoint);
        }
        else
        {
            MoveToPoint(GetPlayerPosition());
        }
    }

    void MoveToPoint(Vector2 point)
    {
        Debug.DrawLine(transform.position, point, Color.white);
        Vector2 dir = point - (Vector2)transform.position;
        float angle = Vector2.Angle(dir, Vector2.down);
        if (transform.position.x > point.x)
            angle *= -1;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }

    private void SetPointsToMove(int pointsCount)
    {
        for (int i = 0; i < pointsCount; i++)
            movePoints.Enqueue(new Vector2(Random.Range(-2.6f, 2.6f), Random.Range(2f, 4f)));
    }

    private void SetBallsRandom()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            int val = Random.Range(0, 9);
            result += val;
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.blueNumbers[val];
        }
        transform.GetChild(numberOfBalls - 1).GetComponent<Rigidbody2D>().mass = 20f;
        transform.GetChild(numberOfBalls - 1).GetComponent<Rigidbody2D>().angularDrag = 0.06f;
    }

}
