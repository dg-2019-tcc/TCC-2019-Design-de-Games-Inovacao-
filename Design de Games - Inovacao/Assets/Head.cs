using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float headForce;

    private Rigidbody2D ballrb;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bola"))
        {
            ballrb = col.GetComponent<Rigidbody2D>();

            ballrb.AddForce(new Vector2(headForce, Random.Range(-5f, 5f)), ForceMode2D.Impulse);
        }
    }
}

