using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrack : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Input.mousePosition;
        if (mousePosition.y < Screen.height * 0.1f)
        {
            rb.AddForce(new Vector2(0, -100));
        }
        else if (mousePosition.y > Screen.height * 0.9f)
        {
            rb.AddForce(new Vector2(0, 100));
        }
    }
}
