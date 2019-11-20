using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float shipSpeed;

    [Header("For shoot")]
    public Transform spawnBulletPosition;
    public GameObject bulletPrefab;
    public float shootRange;


    // Start is called before the first frame update
    void Start()
    {
        
    }

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
}
