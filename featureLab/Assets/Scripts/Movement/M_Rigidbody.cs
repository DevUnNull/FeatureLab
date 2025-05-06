using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Rigidbody : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(h, v).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
