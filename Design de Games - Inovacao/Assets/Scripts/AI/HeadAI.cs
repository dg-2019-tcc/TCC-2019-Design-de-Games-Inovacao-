using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAI : MonoBehaviour
{
    public float headForceX;

    public float headForceY;

    public Rigidbody2D ballrb;
    private AITriggerController triggerController;
    public StateController controller;
    public bool isFut;
    public bool isVolei;
    private void Awake()
    {
        triggerController = GetComponent<AITriggerController>();
    }

    private void Update()
    {
        if (isFut)
        {
            if (triggerController.triggerCollision.touchBall && triggerController.triggerCollision.naArea == false)
            {
                Chuta();
            }

            else if (triggerController.triggerCollision.chutouBall && triggerController.triggerCollision.naArea == false)
            {
                ChutaForte();
            }

            else if (triggerController.triggerCollision.naArea && triggerController.triggerCollision.chutouBall || triggerController.triggerCollision.touchBall)
            {
                Debug.Log("NaArea");
                NaArea();
            }
        }

        if (isVolei)
        {
            if (triggerController.triggerCollision.touchBall)
            {
                Corta();
            }
        }
    }


    public void Corta()
    {
        ballrb = triggerController.rbBola;
        ballrb.velocity = new Vector2(0, 0);

        ballrb.AddForce(new Vector2(headForceX * 10, headForceY * 15), ForceMode2D.Impulse);
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

    public void NaArea()
    {
        ballrb = triggerController.rbBola;
        ballrb.AddForce(new Vector2(headForceX, 0), ForceMode2D.Impulse);
    }
}
