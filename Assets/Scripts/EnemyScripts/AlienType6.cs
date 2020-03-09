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
    public Vector2 movePoint;
    public float rotationSpeed;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        SetBallsRandom(numberOfBalls);

        //Set move speed, given the number of balls.
        //Speed should be greater if there are more balls.
        SetMoveSpeed(numberOfBalls);

        //Deactive remain balls
        for (int i = numberOfBalls; i < 10; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }

    public void EnemyMovement()
    {
        


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
            movePoint = GetPlayerPosition();
            MoveToPoint(GetPlayerPosition());
        }
    }

    void MoveToPoint(Vector2 point)
    {
        Debug.DrawLine(transform.position, point, Color.white);

        Vector2 dir = movePoint - (Vector2)transform.position;

        //moving with rb
        //rb.MovePosition(rb.position + dir.normalized * moveSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + (-(Vector2)transform.up * moveSpeed * Time.deltaTime));

        //Rotating with rb
        float angle = Vector2.Angle(dir, Vector2.down);
        if (transform.position.x > point.x) angle *= -1;
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime));
    }

    private void SetPointsToMove(int pointsCount)
    {
        for (int i = 0; i < pointsCount; i++)
            movePoints.Enqueue(new Vector2(Random.Range(-2.6f, 2.6f), Random.Range(2f, 4f)));
    }

    private void SetBallsRandom(int balls)
    {
        for (int i = 0; i < balls; i++)
        {
            int val = Random.Range(0, 9);
            result += val + 1;
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = NumbersController._instance.blueNumbers[val];
        }
    }

    private void SetMoveSpeed(int balls)
    {
        if (balls < 4) moveSpeed = 1f;
        else if (balls >= 4 && balls < 7) moveSpeed = 2f;
        else moveSpeed = 3f;
    }
}
