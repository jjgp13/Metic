using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeForce : MonoBehaviour
{

    Rigidbody2D rb;
    AlienType6 parent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parent = transform.GetComponentInParent<AlienType6>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = rb.position - parent.movePoint;
    }
}
