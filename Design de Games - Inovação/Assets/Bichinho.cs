using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bichinho : MonoBehaviour
{
    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb = other.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(10, 5), ForceMode2D.Impulse);
        }
    }
}
