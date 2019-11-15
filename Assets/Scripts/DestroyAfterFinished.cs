using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFinished : MonoBehaviour
{

    public int timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }
}
