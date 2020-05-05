using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAI : MonoBehaviour
{
    public float corteForceX;
    public float corteForceY;

    private Rigidbody2D ballrb;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Volei"))
        {
            ballrb = col.GetComponent<Rigidbody2D>();
            ballrb.AddForce(new Vector2(corteForceX, corteForceY), ForceMode2D.Impulse);
        }
    }
}
