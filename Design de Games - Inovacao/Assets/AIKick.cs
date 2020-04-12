using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIKick : MonoBehaviour
{
    public StateController controller;

    private Rigidbody2D ballrb;

    private void OnTriggerEnter2D(Collider2D col)
    {
        ballrb = col.GetComponent<Rigidbody2D>();

        ballrb.AddForce(new Vector2(controller.botStats.kickForceX, controller.botStats.kickForceY), ForceMode2D.Impulse);
    }
}
