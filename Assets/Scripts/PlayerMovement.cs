using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("For life")]
    public int lifes;
    public GameObject[] lifeSprites;

    public float shipSpeed;
    [Header("For shoot")]
    public Transform spawnBulletPosition;
    public GameObject bulletPrefab;
    public float shootRange;


    // Update is called once per frame
    void Update()
    {
        ShipMovement();
    }

    private void ShipMovement()
    {
        if (EnemiesController._instance.enemiesInField.ContainsKey(NumbersController._instance.result))
        {
            GameObject enemy = EnemiesController._instance.enemiesInField[NumbersController._instance.result];
            transform.position = Vector2.Lerp(
                transform.position,
                new Vector2(enemy.transform.position.x, transform.position.y),
                shipSpeed);
            if (CloseToShoot(transform.position.x, enemy.transform.position.x))
            {
                NumbersController._instance.ClearResult();
                Shoot();
                return;
            }
        }
        else
        {
            //Move Towards the closest enemy
        }
    }

    private bool CloseToShoot(float playerPos, float enemyPos)
    {
        if (Mathf.Abs(playerPos - enemyPos) > -shootRange && Mathf.Abs(playerPos - enemyPos) < shootRange)
            return true;
        else
            return false;
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(lifes < 0)
            {
                Debug.Log("You're dead");
                return;
            }
            lifes--;
            lifeSprites[lifes].SetActive(false);
        }
    }
}
