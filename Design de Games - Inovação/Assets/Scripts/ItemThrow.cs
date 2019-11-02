using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemThrow : MonoBehaviour
{

    public Photon.Realtime.Player Owner { get; private set; }

    public float speed = 100f;
    public Rigidbody2D rb;

    private Vector2 shootDirection;

    public float timeDestroy;

    public float speedPlayer = 1.0f;

    public static Transform totemTarget;

    public bool hit;



    public void InitializeBullet(Photon.Realtime.Player owner)
    {
        Owner = owner;

        shootDirection = ThrowObject.direction;

        rb.velocity = shootDirection;
        rb.position += rb.velocity;

        PlayerMovement.atirou = true;
    }

    private void Update()
    {
        
        timeDestroy += Time.deltaTime;
        if (timeDestroy >= 5f)
        {
            PlayerMovement.atirou = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Pipa"))
        {
            PlayerMovement.dogPipa = true;
            //tokenSom.Play();
            totemTarget = collision.transform;
            PlayerMovement.acertouTotem = true;
            timeDestroy = 4.9f;
        }

        if (collision.CompareTag("Carrinho"))
        {
            PlayerMovement.dogCarro = true;
            //tokenSom.Play();
            totemTarget = collision.transform;
            PlayerMovement.acertouTotem = true;
            timeDestroy = 4.9f;

        }

        if (collision.CompareTag("Player"))
        {
            PlayerMovement jogador = collision.GetComponent<PlayerMovement>();
            jogador.levouDogada = true;
            timeDestroy = 4.9f;
        }
    }
}
