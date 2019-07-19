using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerDetector : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Vector2 touchPos = Input.GetTouch(0).position;
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(touchPos);

            transform.position = worldPos;
            //Debug.Log(worldPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
