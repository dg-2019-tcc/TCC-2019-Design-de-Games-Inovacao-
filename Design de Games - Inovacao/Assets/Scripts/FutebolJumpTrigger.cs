using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutebolJumpTrigger : MonoBehaviour
{
    public  Rigidbody2D rb;
    public StateController controller;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("AI"))
        {
            rb = col.GetComponent<Rigidbody2D>();
            controller = col.GetComponent<StateController>();

            rb.AddForce(new Vector2(0, 8f), ForceMode2D.Impulse);

            Debug.Log("Trigger");
        }
    }
}
