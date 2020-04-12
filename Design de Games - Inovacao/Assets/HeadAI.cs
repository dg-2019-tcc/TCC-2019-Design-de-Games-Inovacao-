using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAI : MonoBehaviour
{
    public float headForceX;

    public float headForceY;

    private Rigidbody2D ballrb;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bola"))
        {
            ballrb = col.GetComponent<Rigidbody2D>();

            ballrb.AddForce(new Vector2(headForceX, headForceY), ForceMode2D.Impulse);
        }
    }
}
