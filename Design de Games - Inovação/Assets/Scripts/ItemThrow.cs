using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrow : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    private Vector2 shootDirection;


    public float timeDestroy;


    // Start is called before the first frame update
    private void Awake()
    {
        timeDestroy += Time.deltaTime;
        shootDirection = ThrowObject.direction;

        rb.velocity = shootDirection * speed;
    }

    private void Update()
    {
        if (timeDestroy >= 3f)
        {
            Destroy(this.gameObject);
        }
    }




}
