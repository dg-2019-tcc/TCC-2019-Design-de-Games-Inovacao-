using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAI : MonoBehaviour
{
    public float headForceX;

    public float headForceY;

    private Rigidbody2D ballrb;
    private AITriggerController triggerController;
    public StateController controller;

    private void Awake()
    {
        triggerController = GetComponent<AITriggerController>();
    }

    private void Update()
    {
        if (triggerController.triggerCollision.touchBall)
        {
            Chuta();
        }

        if (triggerController.triggerCollision.chutouBall)
        {
            ChutaForte();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bola"))
        {
            ballrb = col.GetComponent<Rigidbody2D>();

            ballrb.velocity = new Vector2(0, 0);

            ballrb.AddForce(new Vector2(headForceX, headForceY), ForceMode2D.Impulse);
        }
    }

    public void Chuta()
    {
        ballrb = triggerController.rbBola; 
        ballrb.velocity = new Vector2(0, 0);

        ballrb.AddForce(new Vector2(headForceX, headForceY), ForceMode2D.Impulse);
    }

    public void ChutaForte()
    {
        ballrb = triggerController.rbBola;
        ballrb.AddForce(new Vector2(controller.botStats.kickForceX, controller.botStats.kickForceY), ForceMode2D.Impulse);
    }
}
