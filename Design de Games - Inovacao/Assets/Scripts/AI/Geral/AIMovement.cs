using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public GameObject ai;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(rb.velocity.x <= 0)
        {
            Quaternion direction = Quaternion.Euler(0, 180, 0);
            ai.transform.rotation = direction;
        }

        else
        {
            Quaternion direction = Quaternion.Euler(0, 0, 0);
            ai.transform.rotation = direction;
        }
    }
}
