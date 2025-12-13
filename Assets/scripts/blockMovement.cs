using System;
using UnityEngine;

public class blockMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Plate")
        {
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
}
