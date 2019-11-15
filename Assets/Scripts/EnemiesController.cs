using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{

    public static EnemiesController _instance;

    public Dictionary<GameObject, int> enemies;

    public void Awake() => _instance = this;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new Dictionary<GameObject, int>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
