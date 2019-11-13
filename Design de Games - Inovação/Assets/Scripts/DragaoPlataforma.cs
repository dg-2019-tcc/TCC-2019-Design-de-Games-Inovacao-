using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragaoPlataforma : MonoBehaviour
{
    public FloatVariable speed;
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(speed.Value, 0, 0);
        }
    }
}
