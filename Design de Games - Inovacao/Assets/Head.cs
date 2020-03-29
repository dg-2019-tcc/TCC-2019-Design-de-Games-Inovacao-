using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float headForceX;

    public float headForceY;

    private Rigidbody2D ballrb;

    public Joystick joyStick;

    public void Start()
    {
        joyStick = FindObjectOfType<Joystick>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Bola"))
        {
            ballrb = col.GetComponent<Rigidbody2D>();

            float forceVertical = headForceY * joyStick.Vertical;
            float forceHorizontal = headForceX * joyStick.Horizontal;

            ballrb.AddForce(new Vector2(forceHorizontal, forceVertical), ForceMode2D.Impulse);
        }
    }
}

