using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    private StateController controller;


    public GameObject ai;

    public float turnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<StateController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(rb.velocity.x < 0f)
        {
            Quaternion direction = Quaternion.Euler(0, 180, 0);
            ai.transform.rotation = direction;
        }

        else if (rb.velocity.x > 0f)
        {
            Quaternion direction = Quaternion.Euler(0, 0, 0);
            ai.transform.rotation = direction;
        }

        else
        {
            Quaternion direction = Quaternion.Euler(0, 180, 0);
            ai.transform.rotation = direction;
        }
    }


}
