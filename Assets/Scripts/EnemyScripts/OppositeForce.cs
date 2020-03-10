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
        parent = transform.parent.GetComponent<AlienType6>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FaceUp();
        Vector2 dir = rb.position - parent.movePoint;
        rb.velocity = dir.normalized;
    }
    /// <summary>
    /// Rotate to always set the number vertcial
    /// </summary>
    private void FaceUp()
    {
        //float angle = Vector2.Angle(new Vector2(0f, transform.rotation.z), Vector2.up);
        transform.Rotate(Vector3.forward, 0f);
    }
}
