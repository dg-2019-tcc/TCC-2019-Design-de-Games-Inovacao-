using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemThrow : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    private Vector2 shootDirection;

    public float timeDestroy;

    public GameObject player;


    private void Awake()
    {
        
        shootDirection = ThrowObject.direction;

        rb.velocity = shootDirection * speed;

    }

    private void Update()
    {

        timeDestroy += Time.deltaTime;
        if (timeDestroy >= 3f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pipa"))
        {
            PlayerMovement.dogPipa = true;
            //tokenSom.Play();
        }

        if (collision.CompareTag("Carrinho"))
        {
            PlayerMovement.dogCarro = true;
            //tokenSom.Play();
        }
    }
}
